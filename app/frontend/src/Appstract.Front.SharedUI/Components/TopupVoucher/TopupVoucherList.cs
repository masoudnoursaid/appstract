using Appstract.Front.Application.Common.Utilities;
using Appstract.Front.Domain.Models.ApiRequestModels;
using Appstract.Front.Domain.Models.ApiResponseModels;
using Appstract.Front.Domain.Models.TopupVoucher;
using Appstract.Front.Infrastructure.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using MudBlazor;

namespace Appstract.Front.SharedUI.Components.TopupVoucher;

public partial class TopupVoucherList
{
      [Inject] 
    private BaseInfoServices BaseInfo { get; set; } = null!;
    [Inject]
    protected NavigationManager NavMng { get; set; } = null!;
    [Inject]
    private ILogger Logger { get; set; } = null!;

    [Parameter]
    public Func<Pagination, Task<ApiResponseBase<PaginatedResponse<ExecutedVoucher>>>> GetExecutedVoucherList
    {
        get;
        set;
    } = null!;

    [Parameter]
    public Func<string, Task<ApiResponseBase<SubmitVoucherApiResponse>>> SubmitVoucher { get; set; } = null!;

    private bool _isExecutedVouchersListLoading = true;
    private bool _unknownError;
    private ApiResponseBase<SubmitVoucherApiResponse> _submitResponse = new();
    private ApiResponseBase<PaginatedResponse<ExecutedVoucher>> _executedVouchersListResponse = new();
    private string _pinNumber = string.Empty;
    private bool _isSubmitBtnDisable = true;
    private int _tablePagesCount = 0;
    private bool _isMainFormLoaded = true;
    private bool _isVoucherCurrencyDifferent;
    private string _voucherAmount = string.Empty;
    private string _topupAmount = string.Empty;
    private string _balanceBefore = string.Empty;
    private string _balanceAfter = string.Empty;
    private Pagination _executedVouchersListFilter = new();
    private MudTable<ExecutedVoucher> _executedVouchersTable = null!;

    private void PinInputValueChanged(string value)
    {
        _isSubmitBtnDisable = true;
        _pinNumber = value;
        if (value.Replace(" ", string.Empty).Length == 16)
        {
            _isSubmitBtnDisable = false;
        }
    }

    private void NewVoucher()
    {
        _isMainFormLoaded = true;
        _submitResponse = new ApiResponseBase<SubmitVoucherApiResponse>();
        _pinNumber = string.Empty;
        _isSubmitBtnDisable = true;
    }

    private async Task Submit()
    {
        _isMainFormLoaded = false;

        try
        {
            _submitResponse = await SubmitVoucher.Invoke(_pinNumber.Replace(" ", string.Empty));

            if (_submitResponse.Success)
            {
                SubmitVoucherApiResponse data = _submitResponse.Data;
                _isVoucherCurrencyDifferent = _submitResponse.Data.BaseCurrency != data.VoucherCurrency;
                _voucherAmount = $"{data.VoucherAmount.ToFormattedString(data.VoucherCurrency,BaseInfo.UserCulture)}";
                _topupAmount =
                    $"{data.VoucherAmount.ToFormattedString(data.VoucherCurrency,BaseInfo.UserCulture)} @ " +
                    $"{data.ExchangeRate:0.000} = " +
                    $"{(data.VoucherAmount * data.ExchangeRate).ToFormattedString(data.BaseCurrency,BaseInfo.UserCulture)}";

                _balanceBefore = data.BalanceBeforeBaseCurrency.ToFormattedString(data.BaseCurrency, BaseInfo.UserCulture);
                if (_isVoucherCurrencyDifferent)
                {
                    _balanceBefore +=
                        $" ({data.BalanceBeforeVoucherCurrency.ToFormattedString(data.VoucherCurrency,BaseInfo.UserCulture)})";
                }

                _balanceAfter = data.BalanceAfterBaseCurrency.ToFormattedString(data.BaseCurrency, BaseInfo.UserCulture);
                if (_isVoucherCurrencyDifferent)
                {
                    _balanceAfter +=
                        $" ({data.BalanceAfterVoucherCurrency.ToFormattedString(data.VoucherCurrency,BaseInfo.UserCulture)})";
                }
            }
        }
        catch (Exception ex)
        {
            _unknownError = true;
            Logger.LogError(ex, "Error in topup voucher list");
        }

        _isMainFormLoaded = true;
        _isExecutedVouchersListLoading = true;
    }

    private async Task<TableData<ExecutedVoucher>> ReloadServerData(TableState state)
    {
        TableData<ExecutedVoucher> result = new() { Items = new List<ExecutedVoucher>(), TotalItems = 0 };
        _isExecutedVouchersListLoading = true;
        StateHasChanged();

        try
        {
            _executedVouchersListResponse = await GetExecutedVoucherList.Invoke(_executedVouchersListFilter);

            if (_executedVouchersListResponse.Success)
            {
                _tablePagesCount = (int)Math.Ceiling((decimal)_executedVouchersListResponse.Data.TotalCount /
                                                     _executedVouchersListFilter.PageSize);
                result = new TableData<ExecutedVoucher>
                {
                    Items = _executedVouchersListResponse.Data.Items,
                    TotalItems = _executedVouchersListResponse.Data.TotalCount
                };
            }
        }
        catch (Exception ex)
        {
            _unknownError = true;
            Logger.LogError(ex, "Error in topup voucher ReloadServerData");
        }

        _isExecutedVouchersListLoading = false;
        StateHasChanged();
        return result;
    }
}