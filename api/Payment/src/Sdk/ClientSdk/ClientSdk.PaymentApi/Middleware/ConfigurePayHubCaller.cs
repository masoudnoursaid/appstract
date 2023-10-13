using Microsoft.AspNetCore.Builder;

namespace Payment.Sdk.Middleware;

public static class ConfigurePaymentProcess
{
    /// <summary>
    /// Pay hub callback middleware to authenticate your webhook using security methods HMAC.
    /// </summary>
    public static IApplicationBuilder UsePayHubCallBackAuthentication(this IApplicationBuilder app)
    {
        app.UseMiddleware<PaymentHmacAuthentication>();

        return app;
    }
}