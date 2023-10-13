namespace Application.Common.Regex;

public abstract class ValidUrlRegex
{
    public static readonly System.Text.RegularExpressions.Regex HttpUrl
        = new(
            @"^https?:\/\/\w+(\.\w+)*(:[0-9]+)?(\/.*)?$");

    public static readonly System.Text.RegularExpressions.Regex HttpsUrl
        = new(
            @"^(https?:\/\/)[\w.-]+(?:\.[\w\.-]+)+[\w\-\._~:/?#[\]@!\$&'\(\)\*\+,;=.]+$");
}