using RESTCountries.NET.Models;
using RESTCountries.NET.Services;

namespace Appstract.Front.Application.Common.Utilities;

public static class CountryUtil
{
    public static List<Country> GetAll()
    {
        return RestCountriesService.GetAllCountries()
            .Where(w => w.Independent == true).ToList();
    }

    public static string GetFlagByCode(string countryCode)
    {
        Country? country = RestCountriesService.GetCountryByCode(countryCode);
        return country == null ? string.Empty : country.Flag.Svg;
    }

    public static string GetNameByCode(string countryCode)
    {
        Country? country = RestCountriesService.GetCountryByCode(countryCode);
        return country == null ? string.Empty : country.Name.Common;
    }

    public static string GetNameByPhoneNumber(string phoneNumber)
    {
        string countryCode = PhoneUtil.GetCountryCode(phoneNumber);
        Country? country = RestCountriesService.GetCountryByCode(countryCode);
        return country == null ? string.Empty : country.Name.Common;
    }

    public static string GetCurrencySymbolByCurrencyCode(string currencyCode)
    {
        try
        {
            if (currencyCode == "IRR")
            {
                return currencyCode;
            }

            Country? country = RestCountriesService.GetAllCountries()
            .FirstOrDefault(w =>
                w.Currencies != null &&
                w.Currencies.Select(c => c.Key).Contains(currencyCode)
            );
            string? symbol = country is null ? currencyCode : country.Currencies?.FirstOrDefault().Value.Symbol;
            return symbol ?? currencyCode;
        }
        catch
        {
            return string.Empty;
        }
    }
}