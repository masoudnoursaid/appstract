namespace Payment.Common.SDK.Models;

public class HmacTemplate
{
    public const string METHOD = $"{{{nameof(METHOD)}}}";
    public const string PATH = $"{{{nameof(PATH)}}}";
    public const string NONCE = $"{{{nameof(NONCE)}}}";
    public const string HOST = $"{{{nameof(HOST)}}}";
    public const string DATE = $"{{{nameof(DATE)}}}";
    private static string Str => $"{METHOD};{PATH};{NONCE};{HOST};{DATE}";

    public string Render(string nonce, string host, string path, string method, string date)
    {
        var result = Str
            .Replace(NONCE, nonce)
            .Replace(HOST, host)
            .Replace(PATH, path)
            .Replace(METHOD, method)
            .Replace(DATE, date);

        return result;
    }
}