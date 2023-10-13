using Microsoft.AspNetCore.Http;
using Payment.Common.SDK.Consts;
using Payment.Common.SDK.Models;
using Payment.Sdk.Common.Model.Configuration;

namespace Payment.Sdk.Common.Extensions;

public static class PaymentAuthenticationHttpContextExtensions
{
    public static HmacInfoObject GetHmacInfoObject(this HttpContext context, PaymentConfiguration paymentConfiguration)
    {
        var request = context.Request;

        var info = new HmacInfoObject(
            new Uri(paymentConfiguration.ConnectionConfiguration.WebHook)
            , new HttpMethod(request.Method)
            , long.Parse(request.Headers[HmacAuthentication.DATE_HEADER].ToString()!)
            , request.Headers[HmacAuthentication.NONCE_HEADER]);

        return info;
    }
}