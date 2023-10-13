using Application.Common.Consts.Authentication;
using Application.Services.PaymentHttpClient;
using Domain.Common.Hmac;
using Domain.Common.Util;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Infrastructure.Common.PaymentHttpClient;

public class HmacHttpClient : EnhancedHttpClient, IHmacHttpClient
{
    private readonly ILogger<HmacHttpClient> _logger;

    protected HmacHttpClient(ILogger<HmacHttpClient> logger, HttpMessageHandler handler) : base(handler)
    {
        _logger = logger;
    }

    public virtual async Task<HttpResponseMessage> PostHmacHttpRequest<TModel>(TModel model, string url, string secret)
    {
        if (string.IsNullOrEmpty(url) || string.IsNullOrEmpty(secret))
            return null!;

        var uri = new System.Uri(url);
        var ticks = DateTime.UtcNow.Ticks;
        var nonce = Guid.NewGuid().ToString();
        var hmacHash =
            await HmacUtils.ComputeHmacSha256(new HmacInfoObject(uri, HttpMethod.Post, ticks, nonce), secret);

        DefaultRequestHeaders.Add(HmacAuthentication.NONCE_HEADER, nonce);
        DefaultRequestHeaders.Add(HmacAuthentication.DATE_HEADER, ticks.ToString());
        DefaultRequestHeaders.Add(HmacAuthentication.SIGNATURE_HEADER, hmacHash);

        _logger.LogInformation("HMAC_HTTP_CLIENT.POST_HMAC_HTTP_REQUEST --- Url : {url} --- Json : {json}", url,
            JsonConvert.SerializeObject(model));

        var response = await PostJsonAsync(model, uri);

        return response;
    }
}



