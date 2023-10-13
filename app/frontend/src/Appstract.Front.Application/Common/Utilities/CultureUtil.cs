using System.Globalization;
using Appstract.Front.Application.Common.Constants;

namespace Appstract.Front.Application.Common.Utilities;

public static class CultureUtil
{
    private static readonly Dictionary<string, CultureInfo> _cashedCultures = new();

    private static readonly Dictionary<string, string> _cashedCurrencyCodeCultureName = new()
    {
        { "IRR", "fa-IR" }, { "USD", "en-US" }, { "EUR", "nl-NL" }, { "MYR", "en-MY" },
    };

    public static CultureInfo? GetByCountryCode(string countryCode)
    {
        try
        {
            if (string.IsNullOrEmpty(countryCode))
            {
                return null;
            }

            if (!CountryCodeCultureName.All.TryGetValue(countryCode, out string? cultureName))
            {
                cultureName =
                    CultureInfo.GetCultures(CultureTypes.AllCultures)
                        .First(c =>
                        {
                            try
                            {
                                return new RegionInfo(c.LCID).TwoLetterISORegionName == countryCode;
                            }
                            catch
                            {
                                return false;
                            }
                        }).Name;
                CountryCodeCultureName.All.Add(countryCode, cultureName);
            }

            CultureInfo? culture = GetByName(cultureName);

            return culture;
        }
        catch
        {
            return null;
        }
    }

    public static CultureInfo? GetByCurrencyCode(string currencyCode)
    {
        try
        {
            if (string.IsNullOrEmpty(currencyCode))
            {
                return null;
            }

            if (!_cashedCurrencyCodeCultureName.TryGetValue(currencyCode, out string? cultureName))
            {
                cultureName =
                    CultureInfo.GetCultures(CultureTypes.AllCultures)
                        .First(c =>
                        {
                            try
                            {
                                return new RegionInfo(c.Name).ISOCurrencySymbol == currencyCode;
                            }
                            catch
                            {
                                return false;
                            }
                        }).Name;
                _cashedCurrencyCodeCultureName.Add(currencyCode, cultureName);
            }

            CultureInfo? culture = GetByName(cultureName);

            return culture;
        }
        catch
        {
            return null;
        }
    }

    public static CultureInfo? GetByName(string name)
    {
        try
        {
            if (string.IsNullOrEmpty(name))
            {
                return null;
            }

            if (_cashedCultures.TryGetValue(name, out CultureInfo? result))
            {
                return result;
            }

            switch (name)
            {
                case "fa-IR":
                    result = new CultureInfo("fa-IR")
                    {
                        NumberFormat = { CurrencyGroupSeparator = ",", CurrencyDecimalSeparator = "." }
                    };
                    break;
                case "nl-NL":
                    result = new CultureInfo("nl-NL")
                    {
                        NumberFormat = { CurrencyGroupSeparator = ".", CurrencyDecimalSeparator = "," }
                    };
                    break;
                default:
                    CultureInfo? culture = CultureInfo.GetCultures(CultureTypes.AllCultures)
                        .FirstOrDefault(c => c.Name == name);
                    result = culture is null ? null : new CultureInfo(culture.Name);
                    break;
            }

            if (result is null)
            {
                return result;
            }

            string symbol = string.Empty;
            try
            {
                symbol = CountryUtil.GetCurrencySymbolByCurrencyCode(new RegionInfo(result.Name).ISOCurrencySymbol);
            }
            catch
            {
                // ignored
            }

            if (!string.IsNullOrWhiteSpace(symbol))
            {
                result.NumberFormat.CurrencySymbol = symbol;
            }

            _cashedCultures.Add(result.Name, result);

            return result;
        }
        catch
        {
            return null;
        }
    }

    public static string GetCurrencyCode(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return string.Empty;
        }

        RegionInfo reg = new(name);
        return reg.ISOCurrencySymbol;
    }
}