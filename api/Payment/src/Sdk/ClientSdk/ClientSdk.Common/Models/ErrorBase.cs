using Payment.Common.SDK.Contracts;
using Payment.Common.SDK.Enums;

#pragma warning disable CS8618

namespace Payment.Common.SDK.Models;

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