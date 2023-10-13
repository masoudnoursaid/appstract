namespace Application.Common.Extensions;

public static class StringExtensions
{
    public static string? NANToNull(this string str)
    {
        return str.Equals("NAN", StringComparison.OrdinalIgnoreCase) ? null : string.Empty;
    }
}