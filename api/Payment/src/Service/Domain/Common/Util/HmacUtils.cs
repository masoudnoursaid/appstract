using System.Security.Cryptography;
using System.Text;
using Domain.Common.Hmac;

namespace Domain.Common.Util;

public abstract class HmacUtils
{
    private static HMACSHA256 CreateHMACSha256(string key)
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

    public static string ComputeHMACSha256(string key, string data)
    {
        var encoding = new ASCIIEncoding();
        var keyBytes = encoding.GetBytes(key);
        var messageBytes = encoding.GetBytes(data);
        var cryptographer = new HMACSHA256(keyBytes);
        var bytes = cryptographer.ComputeHash(messageBytes);
        var result = BitConverter.ToString(bytes).Replace("-", "").ToLower();
        return result;
    }

    public static Task<string> ComputeHmacSha256(HmacInfoObject info, string secret)
    {
        var strToHash = info.ToString();
        var hmacHash = ComputeHashToBase64(secret, strToHash);
        return Task.FromResult(hmacHash);
    }
}