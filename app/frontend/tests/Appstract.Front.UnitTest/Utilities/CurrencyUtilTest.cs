using Appstract.Front.Application.Common.Utilities;
using Appstract.TestCommon.Base;
using FluentAssertions;

namespace UltraTone.UnitTest.Utilities;

public class CurrencyUtilTest : ComponentTestContext
{
    [Theory]
    [InlineData(1500.26, "USD", "fa-IR", "$ 1,500.26")]
    [InlineData(1500.26, "EUR", "fa-IR", "€ 1,500.26")]
    [InlineData(1500.26, "EUR", "nl-NL", "€ 1.500,26")]
    [InlineData(1500.26, "MYR", "fa-IR", "RM 1,500.26")]
    [InlineData(1500.26, "MYR", "en-MY", "RM 1,500.26")]
    [InlineData(1500.26, "IRR", "fa-IR", "IRR 1,500")]
    [InlineData(1500.26, "IRR", "nl-NL", "IRR 1.500")]
    [InlineData(1500.266, "USD", "en-US", "$ 1,500.26")]
    [InlineData(1500.266, "", "en-US", "1500.26")]
    [InlineData(1500.266, "USD", "", "1500.26")]
    [InlineData(1500.266, "", "", "1500.26")]
    [InlineData(1500.266, "***", "***", "1500.26")]
    [InlineData(0, "USD", "en-US", "$ 0.00")]
    [InlineData(0, "", "en-US", "0")]
    [InlineData(0, "USD", "", "0")]
    public void GetFormattedStringOfCurrency(decimal amount, string currencyCode, string cultureName, string expected)
    {
        amount.ToFormattedString(currencyCode, cultureName).Should().Be(expected);
    }

    [Theory]
    [InlineData(null, "USD", "en-US", "$ 0.00")]
    public void GetFormattedStringOfNullableCurrency(decimal? amount, string currencyCode, string cultureName,
        string expected)
    {
        amount.ToFormattedString(currencyCode, cultureName).Should().Be(expected);
    }
}