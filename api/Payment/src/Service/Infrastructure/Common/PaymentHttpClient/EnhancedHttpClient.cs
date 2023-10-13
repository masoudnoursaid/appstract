using System.Text;
using Application.Services.PaymentHttpClient;
using Newtonsoft.Json;

namespace Infrastructure.Common.PaymentHttpClient;

public class EnhancedHttpClient : HttpClient, IEnhancedHttpClient
{
    protected EnhancedHttpClient(HttpMessageHandler handler) : base(handler)
    {
    }

    public EnhancedHttpClient()
    {
    }
    
    public async Task<HttpResponseMessage> PostJsonAsync<TModel>(TModel model, System.Uri uri)
    {
        var json = JsonConvert.SerializeObject(model);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        BaseAddress = new System.Uri(uri.GetLeftPart(UriPartial.Authority));
        var result = await PostAsync(uri.PathAndQuery, content);
        return result;
    }
}