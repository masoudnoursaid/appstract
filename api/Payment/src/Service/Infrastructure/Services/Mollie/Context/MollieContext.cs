using Application.Common.BaseTypes.Context;
using Infrastructure.Common.PaymentHttpClient.Handler;

namespace Infrastructure.Services.Mollie.Context;

public class MollieContext : IPaymentContext
{
    public string ApiKey { get; private set; }
    public bool IsLive { get; private set; }
    public HttpClient MollieHttpClient { get; private set; }

    public MollieContext(string apiKey, bool isLive)
    {
        ApiKey = apiKey;
        IsLive = isLive;

        MollieHttpClient = new HttpClient(new RetryHandler(new HttpClientHandler(), maxRetries: 5));
    }
}