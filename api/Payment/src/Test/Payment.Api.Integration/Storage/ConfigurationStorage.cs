using Infrastructure.Persistence.Sql.Seeds;
using Payment.Sdk.Common.Enum;
using Payment.Sdk.Common.Model.Configuration;

namespace Payment.Api.Integration.Storage;

public static class ConfigurationStorage
{
    public static PaymentConfiguration PaymentConfiguration(string address)
    {
        return new PaymentConfiguration
        {
            ConnectionConfiguration = new ConnectionConfiguration
            {
                Address = address,
                Timeout = 50
            },
            SecurityConfiguration = new SecurityConfiguration
            {
                Method = SecurityMethod.HMAC,
                ApiKey = ApplicationSeedStorage.Key.Value,
                ApiSecret = ApplicationSeedStorage.Secret.Value,
                BanAccountAfterSpecificNumbersOfTry = 10
            }
        };
    }
}