using System.Runtime.Serialization;
using ErrorHandling.Abstracts;
using ErrorHandling.Enums;
using ErrorHandling.Interfaces;

namespace Domain.Exceptions.Security;

[Serializable]
public class SecurityException : AppException, ICodedException
{
    public SecurityException() : base("Security Exception.")
    {
    }

    public SecurityException(string message)
        : base(message)
    {
    }

    public SecurityException(string message, Exception inner)
        : base(message, inner)
    {
    }

    public SecurityException(
        SerializationInfo info,
        StreamingContext context)
        : base(info, context)
    {
    }

    public CommonErrorCode GetCommonErrorCode()
    {
        return CommonErrorCode.SecurityError;
    }
}