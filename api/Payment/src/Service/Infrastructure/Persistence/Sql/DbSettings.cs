using Microsoft.Extensions.Configuration;

namespace Infrastructure.Persistence.Sql;

public class DbSettings
{
    [ConfigurationKeyName("DB_APP_HOST")] public string? DbAppHost { get; set; }

    [ConfigurationKeyName("DB_APP_PORT")] public string? DbAppPort { get; set; }

    [ConfigurationKeyName("DB_APP_NAME")] public string? DbAppName { get; set; }

    [ConfigurationKeyName("DB_APP_USER")] public string? DbAppUser { get; set; }

    [ConfigurationKeyName("DB_APP_PASS")] public string? DbAppPass { get; set; }

    [ConfigurationKeyName("DB_APP_VERSION")]
    public string? DbAppVersion { get; set; }

    [ConfigurationKeyName("DB_INIT")] public bool? Migrate { get; set; }
}