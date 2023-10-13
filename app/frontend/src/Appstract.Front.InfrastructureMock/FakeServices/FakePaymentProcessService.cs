using Appstract.Front.Application.Services;
using Appstract.Front.Domain.Models.ApiResponseModels;
using Appstract.Front.Domain.Models.Financial;
using Appstract.Front.Domain.Models.PurchaseHistory;
using Appstract.Web.Infrastructure.MockData;

namespace Appstract.Web.Infrastructure.FakeServices;

public class FakePaymentProcessService: IPaymentProcessService
{
    public Task<ApiResponseBase<CreatePaymentResponse>> CreatePaymentAsync(CreatePaymentRequest request)
    {
        Error error = new() { Code = 11111111, Message = "Content has error" };
        return Task.FromResult(new ApiResponseBase<CreatePaymentResponse> { Success = false, Error = error });
    }

    public async Task<ApiResponseBase<PaginatedResponse<PurchaseHistoryModel>>> GetPurchaseHistoryFilterAsync(PurchaseHistoryRequestFilter request)
    {
        await Task.Delay(300);

        PurchaseHistoryMockData purchaseHistoryMockData = new();
        List<PurchaseHistoryModel> result = await purchaseHistoryMockData.GetPurchaseHistoryFilter(request);
        int count = result.Count;
        result = result.Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize).ToList();
        ApiResponseBase<PaginatedResponse<PurchaseHistoryModel>> response = new()
        {
            Success = true,
            Data = new PaginatedResponse<PurchaseHistoryModel> { Items = result, TotalCount = count }
        };

        return response;
    }

    public async Task<ApiResponseBase<PurchaseHistoryDetailsModel>> GetPurchaseHistoryDetailsAsync(string transactionId)
    {
        await Task.Delay(300);
        PurchaseHistoryMockData purchaseHistoryMockData = new();
        PurchaseHistoryDetailsModel result = await purchaseHistoryMockData.GetPurchaseHistoryDetail(transactionId);
        ApiResponseBase<PurchaseHistoryDetailsModel> response = new() { Success = true, Data = result };
        return response;
    }
}