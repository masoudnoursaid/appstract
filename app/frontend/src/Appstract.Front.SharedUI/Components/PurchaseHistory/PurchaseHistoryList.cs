using Appstract.Front.Application.Common.Constants;
using Appstract.Front.Application.Common.Extensions;
using Appstract.Front.Application.Common.Resources;
using Appstract.Front.Application.Common.Utilities;
using Appstract.Front.Domain.Enums;
using Appstract.Front.Domain.Models.ApiResponseModels;
using Appstract.Front.Domain.Models.PurchaseHistory;
using Appstract.Front.Infrastructure.Services;
using Appstract.Front.SharedUI.Components.Dialogs;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MudBlazor;

namespace Appstract.Front.SharedUI.Components.PurchaseHistory;

public partial class PurchaseHistoryList
{
    [Inject]
    private IDialogService DialogService { get; set; } = null!;

    [Inject]
    private BaseInfoServices BaseInfo { get; set; } = null!;

    [Inject]
    private HttpClientService HttpClientService { get; set; } = null!;

    [Inject]
    private IConfiguration Configuration { get; set; } = null!;

    [Inject]
    private ILogger Logger { get; set; } = null!;

    [Parameter]
    public Func<PurchaseHistoryRequestFilter, Task<ApiResponseBase<PaginatedResponse<PurchaseHistoryModel>>>>
        GetPurchaseHistoryFilter { get; set; } = null!;

    [Parameter]
    public Func<string, Task<ApiResponseBase<PurchaseHistoryDetailsModel>>> GetPurchaseHistoryDetails { get; set; } = null!;
    
    private PurchaseHistoryRequestFilter Filter { get; set; } = new();
    private Error _error = new();
    private bool _showFilters;
    private MudTable<PurchaseHistoryModel> _table = null!;
    private int _tablePagesCount = 0;
    private bool _isTableLoading = true;
    private DateRange? _dates;
    private List<PaymentStatusType> _selectedStatuses = new();
    private bool _unknownError;
    private bool _isClearFiltersBtnDisabled = true;

    private bool CheckHasFilters()
    {
        return _dates?.Start is not null || _dates?.End is not null || _selectedStatuses.Any();
    }
    
    private void ClearFilters()
    {
        _dates = null;
        _selectedStatuses = new List<PaymentStatusType>();
        _table.ReloadServerData();
    }
    
    private async Task<TableData<PurchaseHistoryModel>> ReloadServerData(TableState state)
    {
        TableData<PurchaseHistoryModel> result = new() { Items = new List<PurchaseHistoryModel>(), TotalItems = 0 };
        _error = new();
        _isTableLoading = true;
        _isClearFiltersBtnDisabled = !CheckHasFilters();
        StateHasChanged();

        try
        {
            if (CheckHasFilters())
            {
                Filter.PageNumber = 1;
            }
            Filter.FromDate = _dates?.Start.ToUtc(BaseInfo.UserTimeZone);
            Filter.ToDate = _dates?.End?.AddDays(1).AddSeconds(-1).ToUtc(BaseInfo.UserTimeZone);
            Filter.Status = _selectedStatuses.Select(x => x.ToString()).ToList();
            ApiResponseBase<PaginatedResponse<PurchaseHistoryModel>> response =
                await GetPurchaseHistoryFilter.Invoke(Filter);

            if (response.Success)
            {
                _tablePagesCount = (int)Math.Ceiling((decimal)response.Data.TotalCount / Filter.PageSize);
                result = new TableData<PurchaseHistoryModel>
                {
                    Items = response.Data.Items, TotalItems = response.Data.TotalCount
                };
            }
            else
            {
                _error = response.Error;
            }
        }
        catch (Exception ex)
        {
            _unknownError = true;
            Logger.LogError(ex, "Error while loading purchase history");
        }

        _isTableLoading = false;
        StateHasChanged();
        return result;
    }

    private static Color GetStatusColorClass(string status) => status.ToEnum(typeof(PaymentStatusType)) switch
    {
        PaymentStatusType.Successful => Color.Success,
        PaymentStatusType.Failed => Color.Error,
        PaymentStatusType.Cancelled => Color.Default,
        _ => Color.Default
    };

    private static string GetStatusIcon(string status) => status.ToEnum(typeof(PaymentStatusType)) switch
    {
        PaymentStatusType.Successful => UltraToneIcons.CHECK_CIRCLE,
        PaymentStatusType.Failed => UltraToneIcons.ERROR,
        PaymentStatusType.Cancelled => UltraToneIcons.WARNING,
        _ => ""
    };

    private async Task ShowDetailsPage(string transactionId)
    {
        try
        {
            ApiResponseBase<PurchaseHistoryDetailsModel> response =
                await GetPurchaseHistoryDetails.Invoke(transactionId);
            if (response.Success)
            {
                DialogOptions options = new() { CloseOnEscapeKey = true, NoHeader = true };
                DialogParameters parameters = new() { ["Content"] = response.Data };
                await DialogService.ShowAsync<PurchaseHistoryDetailsDialog>(
                    PurchaseHistoryResource.Details, 
                    parameters,
                    options);
            }
            else
            {
                _error = response.Error;
                StateHasChanged();
            }
        }
        catch (Exception ex)
        {
            _unknownError = true;
            Logger.LogError(ex, "Error in purchase history");
        }
    }

    private async Task ShowSharePage(string transactionId)
    {
        try
        {
            string userCulture = BaseInfo.UserCulture;
            ApiResponseBase<PurchaseHistoryDetailsModel>
                response = await GetPurchaseHistoryDetails.Invoke(transactionId);
            if (response.Success)
            {
                string contentStr =
                    $"Payment {response.Data.PaymentStatusName.ToDescriptionString()}\r\n"
                    + $"Amount: {response.Data.PaymentAmountInOrderCurrency.ToFormattedString(response.Data.PaymentCurrency, userCulture)}\r\n"
                    + (response.Data.PaymentStatusName == PaymentStatusType.Successful
                        ? $"Balance before: {response.Data.BalanceBefore.ToFormattedString(response.Data.BaseCurrency, userCulture)}\r\n"
                        : string.Empty)
                    + (response.Data.PaymentStatusName == PaymentStatusType.Successful
                        ? $"Balance after: {response.Data.BalanceAfter.ToFormattedString(response.Data.BaseCurrency, userCulture)}\r\n"
                        : string.Empty)
                    + (response.Data.BaseCurrency != response.Data.PaymentCurrency &&
                       response.Data.PaymentStatusName == PaymentStatusType.Successful
                        ? $"Exchange rate: {response.Data.PaymentCurrency} to {response.Data.BaseCurrency} = {response.Data.ExchangeRate:0.##}\r\n"
                        : string.Empty)
                    + (response.Data.PaymentStatusName == PaymentStatusType.Successful &&
                       response.Data.BaseCurrency != response.Data.PaymentCurrency
                        ? $"Purchase amount: {response.Data.PaymentAmountInOrderCurrency.ToFormattedString(response.Data.PaymentCurrency, userCulture)} @ {response.Data.ExchangeRate:0.##} = {response.Data.PaymentAmountInBaseCurrency.ToFormattedString(response.Data.BaseCurrency, userCulture)}\r\n"
                        : string.Empty)
                    + $"Transaction ID: {response.Data.TrxKey}\r\n"
                    + $"External ID: {response.Data.ReferenceId}\r\n";

                DialogOptions options = new() { CloseOnEscapeKey = true, NoHeader = true };
                DialogParameters parameters = new() { ["Content"] = contentStr };
                await DialogService.ShowAsync<ShareViaDialog>(PurchaseHistoryResource.Share, parameters, options);
            }
            else
            {
                _error = response.Error;
            }
        }
        catch (Exception ex)
        {
            _unknownError = true;
            Logger.LogError(ex, "Error in purchase history");
        }
    }
}