using Appstract.Front.Domain.Models.ApiResponseModels;
using Appstract.Front.Domain.Models.Financial;

namespace Appstract.Front.Application.Services;

public interface IPaymentMethodService
{
    Task<ApiResponseBase<PaginatedResponse<PaymentMethod>>> GetPaymentMethodsAsync();
}