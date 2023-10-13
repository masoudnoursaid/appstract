using Appstract.Front.Application.Common.Constants;
using Appstract.Front.Application.Services;
using Appstract.Front.Domain.Models.ApiResponseModels;
using Appstract.Front.Domain.Models.Financial;
using Microsoft.AspNetCore.Components;

namespace Appstract.Front.SharedUI.Pages.Financial;

public partial class BuyCredit
{
    private const string ROUTE = Routes.BUY_CREDIT;

    [Inject]
    private IPaymentMethodService PaymentMethodService { get; set; } = null!;
    
  

    private PaymentMethod? _selectedPaymentMethod;
    private bool _isTransformed;

    private void UpdatePaymentMethod(PaymentMethod paymentMethod)
    {
        _selectedPaymentMethod = paymentMethod;
        _isTransformed=true;
        StateHasChanged();
    }

    private async Task<ApiResponseBase<PaginatedResponse<PaymentMethod>>> GetPaymentMethodsAsync()
    {
        return await PaymentMethodService.GetPaymentMethodsAsync();
    }

    private async Task<ApiResponseBase<CreatePaymentResponse>> CreatePaymentAsync(CreatePaymentRequest paymentRequest)
    {
       // return await PaymentProcessService.CreatePaymentAsync(paymentRequest);
       return new ApiResponseBase<CreatePaymentResponse>();
    }

    private void CloseSecondLayer()
    {
        _isTransformed = true;
    }
}