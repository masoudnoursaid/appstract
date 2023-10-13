using Application.Settings.Environments;
using Application.Settings.Environments.Stripe;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Settings.Stripe;

public sealed class StripeEnvironmentSetting : EnvironmentSetting, IStripeEnvironmentSetting
{
    [ConfigurationKeyName("STRIPE_SANDBOX_SECRET_KEY")]
    public string? SandBoxSecretKey { get; set; }

    [ConfigurationKeyName("STRIPE_SANDBOX_PUBLISHABLE_KEY")]
    public string? SandBoxPublishableKey { get; set; }

    [ConfigurationKeyName("STRIPE_SECRET_KEY")]
    public string? SecretKey { get; set; }

    [ConfigurationKeyName("STRIPE_PUBLISHABLE_KEY")]
    public string? PublishableKey { get; set; }
}