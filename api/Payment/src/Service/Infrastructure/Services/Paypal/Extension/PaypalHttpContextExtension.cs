using Infrastructure.Common.Extensions;
using Infrastructure.Services.Paypal.Model;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Services.Paypal.Extension;

public static class PaypalHttpContextExtension
{
    public static PaypalPayerInfoModel GetPaypalPayerInfoModel(this HttpContext context)
    {
        var result = context.ConvertFromQueryString<PaypalPayerInfoModel>();
        return result;
    }
}