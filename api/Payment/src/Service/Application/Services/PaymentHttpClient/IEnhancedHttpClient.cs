namespace Application.Services.PaymentHttpClient;


public interface IEnhancedHttpClient
{
    Task<HttpResponseMessage> PostJsonAsync<TModel>(TModel model, Uri uri);
}
