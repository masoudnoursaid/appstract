namespace Application.Settings.Environments.Paypal;

public interface IPaypalEnvironmentSetting
{
    string? SandBoxClientId { get; set; }
    string? SandBoxClientSecret { get; set; }
    string? LiveClientId { get; set; }
    string? LiveClientSecret { get; set; }
}