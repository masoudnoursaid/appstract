namespace ErrorHandling.Helpers;

public static class ErrorCodeHelper
{
    public static string Format(int code)
    {
        return code.ToString("n0").Replace(",", "_");
    }
}