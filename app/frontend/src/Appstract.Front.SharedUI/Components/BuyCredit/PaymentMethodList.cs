using Appstract.Front.Domain.Models.ApiResponseModels;
using Appstract.Front.Domain.Models.Financial;
using Appstract.Front.Infrastructure.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Appstract.Front.SharedUI.Components.BuyCredit;


public partial class PaymentMethodList
{
    
    [Inject] 
    private HttpClientService HttpClientService { get; set; } = null!;
    
    [Inject]
    private NavigationManager NavManager { get; set; } = null!;

    [Inject]
    private IConfiguration Configuration { get; set; } = null!;
    
    [Inject]
    private ILogger Logger { get; set; } = null!;
    
    [Parameter]
    public EventCallback<PaymentMethod> OnChangePaymentMethod { get; set; }

    [Parameter]
    public Func<Task<ApiResponseBase<PaginatedResponse<PaymentMethod>>>> GetPaymentMethods { get; set; } = null!;

    private bool _isLoaded;
    private bool _hasError;
    private bool _unknownError;
    private PaymentMethod _selectedPaymentMethod = new();
    protected Error Error = new();
    private List<PaymentMethod> PaymentMethodItems { get; set; } = new();

    protected override async Task OnInitializedAsync()
    {
        _unknownError = false;
        await ShowPaymentMethodsListAsync();
    }

    private async Task ShowPaymentMethodsListAsync()
    {
        try
        {
            ApiResponseBase<PaginatedResponse<PaymentMethod>> response = await GetPaymentMethods.Invoke();
            if (response.Success)
            {
                PaymentMethodItems = response.Data.Items;
            }
            else
            {
                _hasError = true;
                Error = response.Error;
            }
        }
        catch (Exception ex)
        {
            _unknownError = true;
            Logger.LogError(ex, "Error in Payment Method List");
        }

        _isLoaded = true;
    }

    private void ChangeSelectedMethod(PaymentMethod paymentMethod)
    {
        _selectedPaymentMethod.Selected = false;
        paymentMethod.Selected = true;
        _selectedPaymentMethod = paymentMethod;
        if (!paymentMethod.Enabled)
        {
            return;
        }
        OnChangePaymentMethod.InvokeAsync(paymentMethod);
    }

    private string GetPaymentMethodClass(PaymentMethod paymentMethod)
    {
        return paymentMethod.Selected ? "payment-method__selected-item" : "payment-method__item";
    }
}