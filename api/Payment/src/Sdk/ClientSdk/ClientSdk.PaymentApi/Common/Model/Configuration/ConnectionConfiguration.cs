#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
namespace Payment.Sdk.Common.Model.Configuration;

public class ConnectionConfiguration
{
    #region [- Public Properties -]

    /// <summary>
    ///     Payment Address
    /// </summary>
    public string? Address { get; set; }

    /// <summary>
    ///     Connection timeout in seconds, default is 60
    /// </summary>
    public int Timeout { get; set; } = 60;

    /// <summary>
    /// Your application hook address, after provider response pay hub will make a post request to this web hook
    /// </summary>
    public string WebHook { get; set; }

    #endregion

    #region [- Internal Properties -]

    internal string Host => Uri.Host;
    internal string PathAndQuery => Uri.PathAndQuery;
    internal Uri Uri => new(Address!);

    #endregion
}