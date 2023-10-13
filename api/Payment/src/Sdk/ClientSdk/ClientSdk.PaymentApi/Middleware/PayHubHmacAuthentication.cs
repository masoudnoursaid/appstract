using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Payment.Common.SDK.Consts;
using Payment.Common.SDK.Exceptions;
using Payment.Common.SDK.Models;
using Payment.Sdk.Common.Extensions;
using Payment.Sdk.Common.Model.Configuration;
using Payment.Sdk.Common.Utils;

namespace Payment.Sdk.Middleware;

public class PaymentHmacAuthentication
{
    private readonly RequestDelegate _next;

    public PaymentHmacAuthentication(RequestDelegate next)
    {
        _next = next;
    }


    public async Task InvokeAsync(HttpContext httpContext
        , [FromServices] ILogger<PaymentHmacAuthentication> logger
        , [FromServices] PaymentConfiguration configuration)
    {
        string path = httpContext.Request.Path;
        if (configuration.ConnectionConfiguration.WebHook.Contains(path, StringComparison.OrdinalIgnoreCase))
        {
            HttpRequest request = httpContext.Request;
            HmacInfoObject info = httpContext.GetHmacInfoObject(configuration);

            string expectedHmacHash = request.Headers[HmacAuthentication.SIGNATURE_HEADER].ToString()!;
            string hmacHash =
                await SecurityUtils.ComputeHmacSha256(info, configuration.SecurityConfiguration.ApiSecret!);

            if (expectedHmacHash == hmacHash)
            {
                logger.LogInformation(
                    "PAYMENT_AUTHENTICATION.MIDDLEWARE --- Hmac authorization successfully done! --- Hmac : {MatchHash}",
                    hmacHash);
            }
            else
            {
                logger.LogError(
                    "PAYMENT_AUTHENTICATION.MIDDLEWARE --- Hmac authorization failed! --- Hmac : {MatchHash}",
                    hmacHash);
                throw new HmacSecurityException(hmacHash, info);
            }
        }

        await _next(httpContext);
    }
}