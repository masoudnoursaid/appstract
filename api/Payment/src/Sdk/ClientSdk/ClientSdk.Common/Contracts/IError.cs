using Payment.Common.SDK.Enums;

namespace Payment.Common.SDK.Contracts;

public interface IError<TEnum>
    where TEnum : Enum
{
    public TEnum Code { get; init; }
    public ClientErrorType Type { get; init; }

    /// <summary>
    ///     Gets messages are the values of error that needs to be replaced in the localized text message.
    ///     With this approach the client can switch language and the translation of the message will be done by the client.
    ///     at the runtime from the resource translation file.
    /// </summary>
    public Dictionary<string, string>? Values { get; init; }
}