using Payment.Sdk.Common.Enum;

namespace Payment.Sdk.Common.Model.Configuration;

public class SecurityConfiguration
{
    /// <summary>
    ///     Protocol security between you and Payment
    /// </summary>
    public SecurityMethod Method { get; set; }

    /// <summary>
    ///     Leave it null if you have no restriction
    /// </summary>
    public int? BanAccountAfterSpecificNumbersOfTry { get; set; }

    /// <summary>
    ///     Your application api secret
    /// </summary>
    public string? ApiSecret { get; set; }


    /// <summary>
    ///     Your application api key
    /// </summary>
    public string? ApiKey { get; set; }
}