using Appstract.Front.Domain.Models.ApiResponseModels;
using Appstract.Front.Domain.Models.Financial;
using Appstract.Front.Domain.Models.PurchaseHistory;

namespace Appstract.Front.Application.Services;

public interface IPaymentProcessService
{
    Task<ApiResponseBase<CreatePaymentResponse>> CreatePaymentAsync(CreatePaymentRequest request);
    Task<ApiResponseBase<PaginatedResponse<PurchaseHistoryModel>>> GetPurchaseHistoryFilterAsync(
        PurchaseHistoryRequestFilter request);
    Task<ApiResponseBase<PurchaseHistoryDetailsModel>> GetPurchaseHistoryDetailsAsync(string transactionId);
}