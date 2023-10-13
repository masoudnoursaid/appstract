using System.Globalization;
using System.Resources;

namespace Payment.Common.SDK.Helpers;

public static class ErrorMessageHelper<TResource, TErrorEnum>
{
    public const string EN_US = "en-US";

    private static readonly ResourceManager _errorCodeResourceManager
        = new($"{typeof(TResource).FullName}", typeof(TResource).Assembly);

    public static string GetByCode(TErrorEnum errorCode, CultureInfo? culture = null)
    {
        culture ??= new CultureInfo(EN_US);
        var message = _errorCodeResourceManager.GetString($"{errorCode}", culture);
        return message ?? string.Empty;
    }

    public static bool TryGetByCode(TErrorEnum errorCode, out string? message, CultureInfo? culture = null)
    {
        culture ??= new CultureInfo(EN_US);
        message = _errorCodeResourceManager.GetString($"{errorCode}", culture);
        return message != null;
    }
}