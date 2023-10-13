using Infrastructure.Common.PaymentHttpClient;
using Infrastructure.Services.PaymentWebHookHttpClient.Dto;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Services.PaymentWebHookHttpClient;

public class PaymentWebHookHttpClient : HmacHttpClient, IPaymentWebHookHttpClient
{
    private readonly ILogger<PaymentWebHookHttpClient> _hookLogger;

    public PaymentWebHookHttpClient(ILogger<PaymentWebHookHttpClient> hookLogger, ILogger<HmacHttpClient> logger,
        HttpMessageHandler handler) : base(logger, handler)
    {
        _hookLogger = hookLogger;
    }

    public async Task<HttpResponseMessage> SendResultToClient(NotifyClientForPaymentResultDto dto, string url,
        string secret)
    {
        try
        {
            var response = await PostHmacHttpRequest(dto, url, secret);

            _hookLogger.LogInformation(
                "PAYMENT_WEB_HOOK_HTTP_CLIENT.SEND_RESULT_TO_CLIENT --- Payment Id : {paymentId} --- Successful : {successful} --- Payment Status : {status} --- Response Status : {responseStatus} --- Client Web Hook : {hook}",
                dto.PaymentId, dto.Successful, dto.Status, response.StatusCode, url);

            return response;
        }
        catch (Exception exception)
        {
            _hookLogger.LogError("PAYMENT_WEB_HOOK_HTTP_CLIENT.SEND_RESULT_TO_CLIENT --- {msg} --- {stack}",
                exception.Message, exception.StackTrace);
        }

        return null!;
    }
}