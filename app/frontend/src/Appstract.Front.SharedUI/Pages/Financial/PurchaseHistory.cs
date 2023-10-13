using Appstract.Front.Application.Common.Constants;
using Appstract.Front.Application.Services;
using Appstract.Front.Domain.Models.ApiResponseModels;
using Appstract.Front.Domain.Models.PurchaseHistory;
using Microsoft.AspNetCore.Components;

namespace Appstract.Front.SharedUI.Pages.Financial;

public partial class PurchaseHistory
{
    private const string ROUTE = Routes.PURCHASE_HISTORY;

    [Inject]
    private IPaymentProcessService PaymentProcessService { get; set; } = null!;

    private async Task<ApiResponseBase<PaginatedResponse<PurchaseHistoryModel>>> GetPurchaseHistoryFilterAsync(PurchaseHistoryRequestFilter request)
    {
        return await PaymentProcessService.GetPurchaseHistoryFilterAsync(request);
    }
    
    private async Task<ApiResponseBase<PurchaseHistoryDetailsModel>> GetPurchaseHistoryDetailsAsync(string transactionId)
    {
        return await PaymentProcessService.GetPurchaseHistoryDetailsAsync(transactionId);
    }
}