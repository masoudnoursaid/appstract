using Application.Settings.Environments;
using Application.Settings.Environments.BillPlz;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Settings.BillPlz;

public class BillPlzEnvironmentSetting : EnvironmentSetting, IBillPlzEnvironmentSetting
{
    [ConfigurationKeyName("BILLPLZ_SANDBOX_API_KEY")]
    public string? SandboxApiKey { get; set; }

    [ConfigurationKeyName("BILLPLZ_SANDBOX_COL_ID")]
    public string? SandboxCollectionId { get; set; }

    [ConfigurationKeyName("BILLPLZ_SANDBOX_SIG_KEY")]
    public string? SandboxSignatureKey { get; set; }


    [ConfigurationKeyName("BILLPLZ_API_KEY")]
    public string? ApiKey { get; set; }

    [ConfigurationKeyName("BILLPLZ_COL_ID")]
    public string? CollectionId { get; set; }

    [ConfigurationKeyName("BILLPLZ_SIG_KEY")]
    public string? SignatureKey { get; set; }
}