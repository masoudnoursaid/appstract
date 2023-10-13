namespace Infrastructure.Common.PaymentHttpClient.Handler;

public class RetryHandler : DelegatingHandler
{
    private readonly int _maxRetries;

    public RetryHandler(HttpMessageHandler innerHandler, int maxRetries = 3)
        : base(innerHandler)
    {
        _maxRetries = maxRetries;
    }

    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        HttpResponseMessage response = null!;
        for (int i = 0; i < _maxRetries; i++)
        {
            response = await base.SendAsync(request, cancellationToken);
            if (response.IsSuccessStatusCode) {
                return response;
            }
        }

        return response;
    }
}