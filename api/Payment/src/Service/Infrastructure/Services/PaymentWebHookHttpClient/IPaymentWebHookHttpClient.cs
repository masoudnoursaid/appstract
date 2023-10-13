using Application.Services.PaymentHttpClient;
using Infrastructure.Services.PaymentWebHookHttpClient.Dto;

namespace Infrastructure.Services.PaymentWebHookHttpClient;

public interface IPaymentWebHookHttpClient : IHmacHttpClient
{
    Task<HttpResponseMessage> SendResultToClient(NotifyClientForPaymentResultDto dto, string url, string secret);
}
