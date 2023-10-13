using Appstract.Front.Application.Services;
using Appstract.Front.Domain.Models.ApiResponseModels;
using Appstract.Front.Domain.Models.Financial;
using Appstract.Web.Infrastructure.MockData;

namespace Appstract.Web.Infrastructure.FakeServices;

public class FakePaymentMethodService : IPaymentMethodService
{
    public async Task<ApiResponseBase<PaginatedResponse<PaymentMethod>>> GetPaymentMethodsAsync()
    {
        return await Task.FromResult(new ApiResponseBase<PaginatedResponse<PaymentMethod>>
        {
            Data = new PaginatedResponse<PaymentMethod>
            {
                Items = await PaymentMethodMockData.GetPaymentMethods()
            },
            Success = true
        });
    }
}