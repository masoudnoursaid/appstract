using Appstract.Front.Domain.Models.ApiRequestModels;
using Appstract.Front.Domain.Models.ApiResponseModels;
using Appstract.Front.Domain.Models.TopupVoucher;

namespace Appstract.Front.Application.Services;

public interface IVoucherService
{
    Task<ApiResponseBase<SubmitVoucherApiResponse>> SubmitTopupVoucherAsync(string voucherCode);
    Task<ApiResponseBase<PaginatedResponse<ExecutedVoucher>>> GetExecutedVouchersAsync(Pagination pagingFilter);
}