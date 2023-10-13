using System.Text;
using Payment.Common.SDK.Utilities;

namespace Payment.Common.SDK.Extensions;

public static class Sha256Extensions
{
    public static string MakeItSha256(this string data)
    {
        var output = Encoding.ASCII.GetBytes(data);

        var base64 = Convert.ToBase64String(output);
        var hash = Sha256.HashStream(StringUtils.GenerateStreamFromString(base64));

        var result = ArrayUtils.ArrayToString(hash);
        return result;
    }
}