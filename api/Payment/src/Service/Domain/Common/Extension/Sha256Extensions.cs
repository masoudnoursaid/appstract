using System.ComponentModel;
using Domain.Common.Hash;
using Domain.Common.Util;

namespace Domain.Common.Extension;

public static class Sha256Extensions
{
    public static string MakeItSha256(this object data)
    {
        var output = (byte[])(TypeDescriptor.GetConverter(data).ConvertTo(data, typeof(byte[])) ??
                              throw new InvalidOperationException("can not convert data to byte array!"));

        var base64 = Convert.ToBase64String(output);
        var hash = Sha256.HashStream(StringUtils.GenerateStreamFromString(base64));

        var result = ArrayUtils.ArrayToString(hash);
        return result;
    }
}