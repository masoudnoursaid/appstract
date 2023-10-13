using Payment.Common.SDK.Models;
using Payment.Common.SDK.Utilities;

namespace Payment.Sdk.Common.Utils;

public class SecurityUtils
{
    public static Task<string> ComputeHmacSha256(HmacInfoObject info, string apiSecret)
    {
        var strToHash = info.ToString();
        var hmacHash = HmacUtils.ComputeHashToBase64(apiSecret, strToHash);
        return Task.FromResult(hmacHash);
    }

    public static async Task<bool> TryMatch(string expectedHmacHash, HmacInfoObject info, string apiSecret)
    {
        var hmacHash = await ComputeHmacSha256(info, apiSecret);
        return expectedHmacHash == hmacHash;
    }
}