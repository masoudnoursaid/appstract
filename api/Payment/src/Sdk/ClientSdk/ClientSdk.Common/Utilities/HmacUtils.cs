using System.Security.Cryptography;
using System.Text;

namespace Payment.Common.SDK.Utilities;

public class HmacUtils
{
    public static HMACSHA256 CreateHMACSha256(string key)
    {
        return new HMACSHA256(Encoding.UTF8.GetBytes(key));
    }

    public static byte[] ComputeHashToByte(string key, string data)
    {
        return CreateHMACSha256(key).ComputeHash(Encoding.UTF8.GetBytes(data));
    }

    public static string ComputeHashToBase64(string key, string data)
    {
        return Convert.ToBase64String(CreateHMACSha256(key).ComputeHash(Encoding.UTF8.GetBytes(data)));
    }
}