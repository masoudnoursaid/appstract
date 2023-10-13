namespace Application.Common.Regex;

public abstract class ValidUUIDRegex
{
    public static readonly System.Text.RegularExpressions.Regex? UUID =
        new("[0-9a-f]{8}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{12}");
}