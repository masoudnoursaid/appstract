using Microsoft.Extensions.Configuration;

namespace Infrastructure.Common.Settings;

public class PayHubEnvironmentSetting
{
    [ConfigurationKeyName("PAYHUB_API_KEY")]
    public string ApiKey { get; set; } = null!;
    
    
    [ConfigurationKeyName("PAYHUB_API_SECRET")]
    public string ApiSecret { get; set; } = null!;
}