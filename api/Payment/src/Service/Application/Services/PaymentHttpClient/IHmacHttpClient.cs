namespace Application.Services.PaymentHttpClient;

public interface IHmacHttpClient
{
    Task<HttpResponseMessage> PostHmacHttpRequest<TModel>(TModel model, string url, string secret);
}

