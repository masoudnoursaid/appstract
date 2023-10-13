using System.Globalization;
using Appstract.Front.Application.Common.Resources;
using Appstract.Front.Application.Common.Utilities;
using Appstract.Front.Domain.Models.ApiResponseModels;
using Appstract.Front.Domain.Models.Financial;
using Appstract.Front.Infrastructure.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MudBlazor;

namespace Appstract.Front.SharedUI.Components.BuyCredit;

public partial class CreditAmount
{
    [CascadingParameter]
    private PaymentMethod? SelectedPaymentMethod { get; set; }

    [Inject]
    private BaseInfoServices BaseInfo { get; set; } = null!;
    
    [Inject]
    private NavigationManager NavManager { get; set; } = null!;

    [Inject]
    private IConfiguration Configuration { get; set; } = null!;
    
    [Inject]
    private ILogger Logger { get; set; } = null!;

    [Parameter]
    public Func<CreatePaymentRequest, Task<ApiResponseBase<CreatePaymentResponse>>> CreatePayment { set; get; } = null!;

    private bool _isLoaded = true;
    private bool _hasError;
    private bool _unknownError;
    protected Error Error = new();
    private decimal? _paymentAmount;
    private string _placeHolder = string.Empty;
    private CultureInfo? _currencyCulture;
    private bool _isCheckoutBtnDisable = true;
    private MudTextField<decimal?> PaymentAmountField { get; set; } = null!;

    private RegexMask PaymentAmountMask =>
        _currencyCulture is null || _currencyCulture!.NumberFormat.CurrencyDecimalDigits == 0
            ? new RegexMask(@"^\d+$")
            : new RegexMask(@$"^\d+(?:\.\d{{0,{_currencyCulture!.NumberFormat.CurrencyDecimalDigits}}})?$");

    private string PaymentAmountHelperText => _paymentAmount > 0
        ? _paymentAmount.ToFormattedString(SelectedPaymentMethod!.Currency, BaseInfo.UserCulture)
        : "";
    
    protected override void OnParametersSet()
    {
        Error = new();
        _placeHolder = SetPlaceHolder();
        _currencyCulture = CultureUtil.GetByCurrencyCode(SelectedPaymentMethod?.Currency?? string.Empty);
    }

    private string SetPlaceHolder()
    {
        if (SelectedPaymentMethod is null ||SelectedPaymentMethod.MinAmount is null || SelectedPaymentMethod.MaxAmount is null)
        {
            return string.Empty;
        }

        string minAmount = SelectedPaymentMethod.MinAmount.ToFormattedString(SelectedPaymentMethod.Currency,BaseInfo.UserCulture, false);
        string maxAmount = SelectedPaymentMethod.MaxAmount.ToFormattedString(SelectedPaymentMethod.Currency,BaseInfo.UserCulture, false);
        return $"{minAmount}~{maxAmount}";
    }
    
    protected string? PaymentAmountValidation(decimal? amount)
    {
        _isCheckoutBtnDisable = true;
        Error = new();
        StateHasChanged();

        if (amount is null)
        {
            return null;
        }

        if (SelectedPaymentMethod?.MinAmount is not null &&
            SelectedPaymentMethod?.MaxAmount is not null &&
            (amount < SelectedPaymentMethod.MinAmount ||
             amount > SelectedPaymentMethod.MaxAmount))
        {
            return string.Format(PaymentMethodResource.ValueRangeMessage, _placeHolder);
        }

        _isCheckoutBtnDisable = false;
        StateHasChanged();
        return null;
    }

    private void ChangeSelectedAmount(long amount)
    {
        _paymentAmount = amount;
        Error = new();
        PaymentAmountValidation(amount);
        PaymentAmountField.ResetValidation();
    }

    private async Task Pay()
    {
        _isLoaded = false;
        if (SelectedPaymentMethod is null)
        {
            _isLoaded = true;
            return;
        }

        if (_paymentAmount is not null)
        {
            try
            {
                ApiResponseBase<CreatePaymentResponse> response = await CreatePayment
                    .Invoke(new CreatePaymentRequest()
                    {
                        PaymentMethod = SelectedPaymentMethod, Amount = (decimal)_paymentAmount
                    });

                if (response.Success)
                {
                    NavManager.NavigateTo(response.Data.CheckoutUrl);
                }
                else
                {
                    Error = response.Error;
                    _isLoaded = true;
                }
            }
            catch (Exception ex)
            {
                _unknownError = true;
                _isLoaded = true;
                Logger.LogError(ex, "Error in Credit Amount");
            }
        }
    }
}