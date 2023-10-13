namespace Application.Common.Regex;

public abstract class ValidEmailRegex
{
    public static readonly System.Text.RegularExpressions.Regex Email
        = new(
            @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
}