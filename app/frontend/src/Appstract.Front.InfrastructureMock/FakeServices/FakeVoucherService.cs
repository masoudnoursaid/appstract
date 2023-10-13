using Appstract.Front.Application.Services;
using Appstract.Front.Domain.Models.ApiRequestModels;
using Appstract.Front.Domain.Models.ApiResponseModels;
using Appstract.Front.Domain.Models.TopupVoucher;
using Appstract.Web.Infrastructure.MockData;

namespace Appstract.Web.Infrastructure.FakeServices;

public class FakeVoucherService : IVoucherService
{
    public async Task<ApiResponseBase<SubmitVoucherApiResponse>> SubmitTopupVoucherAsync(string voucherCode)
    {
        await Task.Delay(1000);

        ApiResponseBase<SubmitVoucherApiResponse> result = await VoucherMockData.Submit(voucherCode);
        return result;
    }

    public async Task<ApiResponseBase<PaginatedResponse<ExecutedVoucher>>> GetExecutedVouchersAsync(
        Pagination pagingFilter)
    {
        await Task.Delay(1000);

        List<ExecutedVoucher> result = await VoucherMockData.GetExecutedVoucherList(pagingFilter);
        int count = result.Count;
        result = result.Skip((pagingFilter.PageNumber - 1) * pagingFilter.PageSize).Take(pagingFilter.PageSize)
            .ToList();

        ApiResponseBase<PaginatedResponse<ExecutedVoucher>> response = await
            Task.FromResult(new ApiResponseBase<PaginatedResponse<ExecutedVoucher>>
            {
                Data = new PaginatedResponse<ExecutedVoucher> { Items = result, TotalCount = count }, Success = true
            });

        return response;
    }
}