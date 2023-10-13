using System.Net.Http.Headers;
using Appstract.Mobile.Application.Common.Consts;
using Sentry;

namespace Appstract.Front.Mobile.Infrastructure.Handler;

public class TokenAuthHeaderHandler : SentryHttpMessageHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        string token = await SecureStorage.Default.GetAsync(SecureStorageLocal.ACCESS_TOKEN);

        if (request.RequestUri?.PathAndQuery != "/auth/refresh-token")
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        return await base.SendAsync(request, cancellationToken);
    }
}