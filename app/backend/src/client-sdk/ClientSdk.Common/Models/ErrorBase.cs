using ClientSdk.Common.Contracts;
using ClientSdk.Common.Enums;

namespace ClientSdk.Common.Models;

public class ErrorBase<TEnum> : IError<TEnum>
    where TEnum : Enum
{
    protected ErrorBase(TEnum code, ClientErrorType type, Dictionary<string, string>? values = null)
    {
        Code = code;
        Type = type;
        Values = values;
    }

    protected ErrorBase()
    {
    }

    public TEnum Code { get; init; }
    public ClientErrorType Type { get; init; }

    /// <inheritdoc>
    ///     <cref>IError</cref>
    /// </inheritdoc>
    public Dictionary<string, string>? Values { get; init; }
}