namespace Application.Settings.Environments.Stripe;

public interface IStripeEnvironmentSetting
{
    string? SandBoxSecretKey { get; set; }
    string? SandBoxPublishableKey { get; set; }
    string? SecretKey { get; set; }
    string? PublishableKey { get; set; }
}