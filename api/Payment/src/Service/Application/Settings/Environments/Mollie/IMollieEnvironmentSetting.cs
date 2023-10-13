namespace Application.Settings.Environments.Mollie;

public interface IMollieEnvironmentSetting
{
    string? SandboxApiKey { get; set; }
    string? ApiKey { get; set; }
    string? PartnerId { get; set; }
    string? ProfileId { get; set; }
}