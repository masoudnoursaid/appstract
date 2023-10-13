namespace Application.Settings.Environments.BillPlz;

public interface IBillPlzEnvironmentSetting
{
    string? SandboxApiKey { get; set; }
    string? SandboxCollectionId { get; set; }
    string? SandboxSignatureKey { get; set; }

    string? ApiKey { get; set; }
    string? CollectionId { get; set; }
    string? SignatureKey { get; set; }
}

