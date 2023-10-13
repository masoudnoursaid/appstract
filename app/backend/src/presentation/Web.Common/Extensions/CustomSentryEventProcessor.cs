using Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Sentry;
using Sentry.Extensibility;

namespace Appstract.Web.Extensions;

public class CustomSentryEventProcessor : ISentryEventProcessor
{
    private readonly IIpService? _ipService;

    public CustomSentryEventProcessor(IHttpContextAccessor httpContextAccessor)
    {
        _ipService = httpContextAccessor.HttpContext?.RequestServices.GetRequiredService<IIpService>();
    }

    public SentryEvent Process(SentryEvent @event)
    {
        var ipAddress = _ipService?.GetRawRemoteIpAddress();
        string? sessionId = null;
        string? cardId = null;
        string? gpn = null;
        string? email = null;


        @event.User = new User
        {
            Id = cardId is null ? null : cardId + ":" + gpn,
            Username = sessionId,
            Email = email,
            IpAddress = ipAddress
        };

        return @event;
    }
}