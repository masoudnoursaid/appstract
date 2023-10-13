using Application.Settings.Environments;
using Application.Settings.Environments.Paypal;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Settings.Paypal;

public sealed class PaypalEnvironmentSetting : EnvironmentSetting, IPaypalEnvironmentSetting
{
    [ConfigurationKeyName("PAYPAL_SANDBOX_CLIENT_ID")]
    public string? SandBoxClientId { get; set; }

    [ConfigurationKeyName("PAYPAL_SANDBOX_CLIENT_SECRET")]
    public string? SandBoxClientSecret { get; set; }


    [ConfigurationKeyName("PAYPAL_LIVE_CLIENT_ID")]
    public string? LiveClientId { get; set; }

    [ConfigurationKeyName("PAYPAL_LIVE_CLIENT_SECRET")]
    public string? LiveClientSecret { get; set; }
}