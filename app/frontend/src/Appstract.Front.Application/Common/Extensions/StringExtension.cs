using System.Diagnostics.CodeAnalysis;

namespace Appstract.Front.Application.Common.Extensions;

[ExcludeFromCodeCoverage]
public static class StringExtension
{
    public static Enum? ToEnum(this string enumValue, Type enumType)
    {
        if (Enum.TryParse(enumType, enumValue, true, out object? result))
        {
            return (Enum)result;
        }

        return null;
    }

    public static string MakeShortFromBegin(this string text, int length)
    {
        return string.IsNullOrWhiteSpace(text) || text.Length <= length ? text : text[..length] + "...";
    }

    public static string MakeShortFromEnd(this string text, int length)
    {
        return string.IsNullOrWhiteSpace(text) || text.Length <= length ? text : "..." + text[^length..];
    }

    public static string MakeShortFromMiddle(this string text, int startLength, int endLength)
    {
        if (string.IsNullOrWhiteSpace(text) || text.Length <= startLength + endLength)
        {
            return text;
        }

        return text[..startLength] + "..." + text[^endLength..];
    }
}