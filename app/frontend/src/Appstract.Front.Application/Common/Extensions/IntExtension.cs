namespace Appstract.Front.Application.Common.Extensions;

public static class IntExtension
{
    private static string ToTimeString(long seconds)
    {
        TimeSpan time = TimeSpan.FromSeconds(seconds);
        return time.Days > 0
            ? time.ToString(@"d\:hh\:mm\:ss")
            : time.ToString(time.Hours > 0 ? @"hh\:mm\:ss" : @"mm\:ss");
    }

    public static string ToTimeString(this int seconds)
    {
        return ToTimeString((long)seconds);
    }

    public static string ToTimeString(this uint seconds)
    {
        return ToTimeString((long)seconds);
    }
}