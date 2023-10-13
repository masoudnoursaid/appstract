namespace Application.Common.Regex;

public abstract class ValidMobileRegex
{
    public static readonly System.Text.RegularExpressions.Regex Mobile
        = new(
            @"^[+]*[(]{0,1}[0-9]{1,4}[)]{0,1}[-\s\./0-9]*$");
}