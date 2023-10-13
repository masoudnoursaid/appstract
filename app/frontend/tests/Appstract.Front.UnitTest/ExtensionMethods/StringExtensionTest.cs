using Appstract.Front.Application.Common.Extensions;
using FluentAssertions;

namespace Appstract.UnitTest.ExtensionMethods;

public class StringExtensionTest
{
    [Theory]
    [InlineData("Test Text", 3, "Tes...")]
    [InlineData("Test", 4, "Test")]
    [InlineData(null, 3, null)]
    public void StringExtension_MakeShortFromBegin_ShouldShouldReturnCorrectValue(string text, int length,
        string expectedText)
    {
        text.MakeShortFromBegin(length).Should().Be(expectedText);
    }

    [Theory]
    [InlineData("Test Text", 3, "...ext")]
    [InlineData("Test", 4, "Test")]
    [InlineData(null, 3, null)]
    public void StringExtension_MakeShortFromEnd_ShouldShouldReturnCorrectValue(string text, int length,
        string expectedText)
    {
        text.MakeShortFromEnd(length).Should().Be(expectedText);
    }

    [Theory]
    [InlineData("Test Text", 3, 3, "Tes...ext")]
    [InlineData("Test", 2, 2, "Test")]
    [InlineData(null, 3, 3, null)]
    public void StringExtension_MakeShortFromMiddle_ShouldShouldReturnCorrectValue(string text, int startLength,
        int endLength, string expectedText)
    {
        text.MakeShortFromMiddle(startLength, endLength).Should().Be(expectedText);
    }
}