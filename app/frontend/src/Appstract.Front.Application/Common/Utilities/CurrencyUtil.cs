using System.Globalization;

namespace Appstract.Front.Application.Common.Utilities;

public static class CurrencyUtil
{
    public static string ToFormattedString(this decimal amount, string currencyCode, string cultureName,
        bool hasSymbol = true)
    {
        try
        {
            CultureInfo? currencyCulture = CultureUtil.GetByCurrencyCode(currencyCode);
            CultureInfo? userCulture = CultureUtil.GetByName(cultureName);
            if (currencyCulture == null || userCulture == null)
            {
                amount = Math.Round(amount, 2, MidpointRounding.ToZero);
                return amount.ToString("0.##");
            }

            CultureInfo culture = new(string.Empty)
            {
                NumberFormat =
                {
                    CurrencySymbol = currencyCulture.NumberFormat.CurrencySymbol,
                    CurrencyDecimalDigits = currencyCulture.NumberFormat.CurrencyDecimalDigits,
                    CurrencyDecimalSeparator = userCulture.NumberFormat.CurrencyDecimalSeparator,
                    CurrencyGroupSeparator = userCulture.NumberFormat.CurrencyGroupSeparator,
                    CurrencyPositivePattern = 2
                }
            };

            amount = Math.Round(amount, currencyCulture.NumberFormat.CurrencyDecimalDigits, MidpointRounding.ToZero);
            return amount.ToString($"{(hasSymbol ? "C" : "N")}{culture.NumberFormat.CurrencyDecimalDigits}", culture);
        }
        catch
        {
            return string.Empty;
        }
    }

    public static string ToFormattedString(this decimal? amount, string currencyCode, string userCulture,
        bool hasSymbol = true)
    {
        return (amount ?? 0).ToFormattedString(currencyCode, userCulture, hasSymbol);
    }
}