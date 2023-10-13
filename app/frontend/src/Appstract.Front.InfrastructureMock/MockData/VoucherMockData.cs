using Appstract.Front.Domain.Models.ApiRequestModels;
using Appstract.Front.Domain.Models.ApiResponseModels;
using Appstract.Front.Domain.Models.TopupVoucher;

namespace Appstract.Web.Infrastructure.MockData;

public static class VoucherMockData
{
     private static readonly List<VoucherItem> _topupVoucherList = new()
    {
        new VoucherItem
        {
            Id = 101,
            CreationDate = new DateTime(2017, 1, 1),
            ExpirationDate = new DateTime(2017, 9, 1),
            Voucher = "1234567123456789",
            Currency = "MYR",
            Credit = (decimal)100.00,
            Activated = true,
            Used = false
        },
        new VoucherItem
        {
            Id = 102,
            CreationDate = new DateTime(2018, 1, 1),
            ExpirationDate = new DateTime(9999, 6, 1),
            Voucher = "1234567987654321",
            Currency = "USD",
            Credit = (decimal)100.00,
            Activated = true,
            Used = false
        },
        new VoucherItem
        {
            Id = 103,
            CreationDate = new DateTime(2018, 1, 1),
            ExpirationDate = new DateTime(9999, 6, 1),
            Voucher = "1234567887654321",
            Currency = "MYR",
            Credit = (decimal)100.00,
            Activated = true,
            Used = true
        },
        new VoucherItem
        {
            Id = 104,
            CreationDate = new DateTime(2018, 1, 1),
            ExpirationDate = new DateTime(9999, 6, 1),
            Voucher = "1234567787654321",
            Currency = "MYR",
            Credit = (decimal)100.00,
            Activated = false,
            Used = true
        }
    };

    private static readonly List<ExecutedVoucher> _voucherHistoryList = new()
    {
        new ExecutedVoucher
        {
            Serial = "1234567787654320",
            Credit = (decimal)100.00,
            Currency = "MYR",
            UsedDate = new DateTime(2017, 1, 1)
        }
    };

    public static async Task<List<ExecutedVoucher>> GetExecutedVoucherList(Pagination pagingFilter)
    {
        int skip = (pagingFilter.PageNumber - 1) * pagingFilter.PageSize;
        return await Task.FromResult(_voucherHistoryList.Skip(skip).Take(pagingFilter.PageSize).ToList());
    }

    public static async Task<ApiResponseBase<SubmitVoucherApiResponse>> Submit(string pin)
    {
        VoucherItem? voucherItem = _topupVoucherList.FirstOrDefault(a => a.Voucher == pin);
        if (voucherItem != null)
        {
            if (!voucherItem.Activated)
            {
                return await Task.FromResult(new ApiResponseBase<SubmitVoucherApiResponse>
                {
                    Success = false, Error = new Error { Message = "Voucher Is Not Active.", Code = 13992103 }
                });
            }

            if (voucherItem.Used)
            {
                return await Task.FromResult(new ApiResponseBase<SubmitVoucherApiResponse>
                {
                    Success = false, Error = new Error { Message = "Voucher Is Used.", Code = 13992102 }
                });
            }

            if (voucherItem.ExpirationDate < DateTime.Now)
            {
                return await Task.FromResult(new ApiResponseBase<SubmitVoucherApiResponse>
                {
                    Success = false, Error = new Error { Message = "Voucher Is Expired.", Code = 13992101 }
                });
            }

            ExecutedVoucher newVoucherHistoryItem = new()
            {
                Serial = voucherItem.Voucher,
                Credit = voucherItem.Credit,
                Currency = voucherItem.Currency,
                UsedDate = DateTime.Now
            };

            _voucherHistoryList.Add(newVoucherHistoryItem);
            voucherItem.Used = true;
            SubmitVoucherApiResponse voucherResult = new()
            {
                BaseCurrency = "MYR",
                VoucherAmount = 100.00m,
                VoucherCurrency = "USD",
                ExchangeRate = 4.555m,
                BalanceBeforeBaseCurrency = 1000.00m,
                BalanceAfterBaseCurrency = 1455.50m,
                BalanceBeforeVoucherCurrency = 1000.00m / 4.555m,
                BalanceAfterVoucherCurrency = 1455.50m / 4.555m,
            };

            return await Task.FromResult(
                new ApiResponseBase<SubmitVoucherApiResponse> { Success = true, Data = voucherResult });
        }

        return await Task.FromResult(new ApiResponseBase<SubmitVoucherApiResponse>
        {
            Success = false, Error = new Error { Message = "Voucher Can Not Be Found.", Code = 13992104 }
        });
    }
}