using Microsoft.Extensions.Configuration;

namespace Infrastructure.Common;

public class SentrySetting
{
    [ConfigurationKeyName("SENTRY_DSN")] public string? Dsn { get; set; }

    [ConfigurationKeyName("SENTRY_ENVIRONMENT")]
    public string? Environment { get; set; }
}