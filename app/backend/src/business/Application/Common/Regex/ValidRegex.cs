namespace Application.Common.Regex;

public abstract class ValidRegex
{
    public static readonly System.Text.RegularExpressions.Regex Email
        = new(
            @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
    
    public static readonly System.Text.RegularExpressions.Regex Mobile
        = new(
            @"^[+]*[(]{0,1}[0-9]{1,4}[)]{0,1}[-\s\./0-9]*$");
    
    public static readonly System.Text.RegularExpressions.Regex HttpUrl
        = new(
            @"^https?:\/\/\w+(\.\w+)*(:[0-9]+)?(\/.*)?$");

}