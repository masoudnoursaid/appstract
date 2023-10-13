using Appstract.Front.Application.Common.Constants;
using Appstract.Front.Application.Services;
using Appstract.Front.Domain.Models.ApiRequestModels;
using Appstract.Front.Domain.Models.ApiResponseModels;
using Appstract.Front.Domain.Models.TopupVoucher;
using Microsoft.AspNetCore.Components;

namespace Appstract.Front.SharedUI.Pages.Financial;

public partial class TopupVoucher
{
    private const string ROUTE = Routes.TOP_UP_VOUCHER;

    [Inject]
    private IVoucherService VoucherService { get; set; } = null!;

    private async Task<ApiResponseBase<PaginatedResponse<ExecutedVoucher>>> GetExecutedVouchersAsync(Pagination pagingFilter)
    {
        return await VoucherService.GetExecutedVouchersAsync(pagingFilter);
    }

    private async Task<ApiResponseBase<SubmitVoucherApiResponse>> SubmitTopupVoucherAsync(string voucherCode)
    {
        return await VoucherService.SubmitTopupVoucherAsync(voucherCode);
    }
}