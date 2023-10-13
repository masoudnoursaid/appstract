using Application.Settings.Environments;
using Application.Settings.Environments.Mollie;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Settings.Mollie;

public class MollieEnvironmentSetting : EnvironmentSetting, IMollieEnvironmentSetting
{
    [ConfigurationKeyName("MOLLIE_SANDBOX_API_KEY")]
    public string? SandboxApiKey { get; set; }

    [ConfigurationKeyName("MOLLIE_LIVE_API_KEY")]
    public string? ApiKey { get; set; }
    
    [ConfigurationKeyName("MOLLIE_PARTNER_ID")]
    public string? PartnerId { get; set; }
    
    [ConfigurationKeyName("MOLLIE_PROFILE_ID")]
    public string? ProfileId { get; set; }
}