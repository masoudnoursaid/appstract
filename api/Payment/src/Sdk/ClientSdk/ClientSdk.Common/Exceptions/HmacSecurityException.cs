using System.Security;
using Payment.Common.SDK.Models;

namespace Payment.Common.SDK.Exceptions;

public class HmacSecurityException : SecurityException
{
    public HmacSecurityException(string hmac, HmacInfoObject info)
        : base($"Invalid hmac : {hmac}\ninfo : {info}")
    {
    }
}