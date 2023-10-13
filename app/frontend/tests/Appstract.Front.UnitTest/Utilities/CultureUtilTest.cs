using System.Collections.Generic;
using System.Globalization;
using Appstract.Front.Application.Common.Utilities;
using Appstract.TestCommon.Base;
using FluentAssertions;

namespace UltraTone.UnitTest.Utilities;

public class CultureUtilTest : ComponentTestContext
{
    private static readonly CultureInfo _faCulture = new("fa-IR")
    {
        NumberFormat = { CurrencyGroupSeparator = ",", CurrencyDecimalSeparator = ".", CurrencySymbol = "IRR", }
    };

    private static readonly CultureInfo _nlCulture = new("nl-NL")
    {
        NumberFormat = { CurrencyGroupSeparator = ".", CurrencyDecimalSeparator = ",", }
    };

    public static IEnumerable<object?[]> CultureNameCultures()
    {
        yield return new object?[] { "fa-IR", _faCulture };
        yield return new object?[] { "nl-NL", _nlCulture };
        yield return new object?[] { "en-US", new CultureInfo("en-US") };
        yield return new object?[] { "en-MY", new CultureInfo("en-MY") };
        yield return new object?[] { "-----", null };
    }

    [Theory]
    [MemberData(nameof(CultureNameCultures))]
    public void WhenTryGetCultureByName_ShouldReturnCorrectCulture(string cultureName, CultureInfo? expected)
    {
        CultureInfo? result = CultureUtil.GetByName(cultureName);
        result.Should().BeEquivalentTo(expected);
        result?.NumberFormat.Should().BeEquivalentTo(expected!.NumberFormat);
    }

    public static IEnumerable<object?[]> CurrencyCodeNumberFormats()
    {
        yield return new object?[] { "IRR", _faCulture.NumberFormat };
        yield return new object?[] { "EUR", _nlCulture.NumberFormat };
        yield return new object?[] { "USD", new CultureInfo("en-US").NumberFormat };
        yield return new object?[] { "MYR", new CultureInfo("en-MY").NumberFormat };
        yield return new object?[] { "---", null };
    }

    [Theory]
    [MemberData(nameof(CurrencyCodeNumberFormats))]
    public void WhenTryGetCultureByCurrencyCode_ShouldReturnCultureWithCorrectValues(string currencyCode,
        NumberFormatInfo? expected)
    {
        CultureInfo? result = CultureUtil.GetByCurrencyCode(currencyCode);
        result?.NumberFormat.CurrencySymbol.Should().Be(expected?.CurrencySymbol);
        result?.NumberFormat.CurrencyDecimalDigits.Should().Be(expected?.CurrencyDecimalDigits);
    }

    public static IEnumerable<object?[]> CountryCodeCultures()
    {
        yield return new object?[] { "IR", _faCulture };
        yield return new object?[] { "NL", _nlCulture };
        yield return new object?[] { "US", new CultureInfo("en-US") };
        yield return new object?[] { "MY", new CultureInfo("en-MY") };
        yield return new object?[] { "--", null };
    }

    [Theory]
    [MemberData(nameof(CountryCodeCultures))]
    public void WhenTryGetCultureByCountryCode_ShouldReturnCorrectCulture(string countryCode, CultureInfo? expected)
    {
        CultureInfo? result = CultureUtil.GetByCountryCode(countryCode);
        result.Should().BeEquivalentTo(expected);
        result?.NumberFormat.Should().BeEquivalentTo(expected!.NumberFormat);
    }

    public static IEnumerable<object?[]> PhoneNumberCultures()
    {
        yield return new object?[] { "+980000000000", _faCulture };
        yield return new object?[] { "+310000000000", _nlCulture };
        yield return new object?[] { "+14845691206", new CultureInfo("en-US") };
        yield return new object?[] { "+600000000000", new CultureInfo("en-MY") };
        yield return new object?[] { "-----", null };
    }

    [Theory]
    [MemberData(nameof(PhoneNumberCultures))]
    public void WhenTryGetCultureByPhoneNumber_ShouldReturnCorrectCulture(string phoneNumber, CultureInfo? expected)
    {
        string countryCode = PhoneUtil.GetCountryCode(phoneNumber);
        CultureInfo? result = CultureUtil.GetByCountryCode(countryCode);
        result.Should().BeEquivalentTo(expected);
        result?.NumberFormat.Should().BeEquivalentTo(expected!.NumberFormat);
    }
    
    [Theory]
    [InlineData("en-MY", "MYR")]
    [InlineData("en-US", "USD")]
    [InlineData("nl-NL", "EUR")]
    [InlineData("fa-IR", "IRR")]
    [InlineData("", "")]
    public void WhenTryGetCurrencyCodeByCultureName_ShouldReturnCorrect(string cultureName, string expectedCurrencyCode)
    {
        string result = CultureUtil.GetCurrencyCode(cultureName);
        result.Should().BeEquivalentTo(expectedCurrencyCode);
    }
}