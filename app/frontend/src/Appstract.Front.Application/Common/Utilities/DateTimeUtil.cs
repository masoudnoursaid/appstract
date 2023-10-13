namespace Appstract.Front.Application.Common.Utilities;

public static class DateTimeUtil
{
    public static DateTime ToUtc(this DateTime localDateTime, string timeZone)
    {
        try
        {
            TimeZoneInfo timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(timeZone);
            return TimeZoneInfo.ConvertTimeToUtc(localDateTime, timeZoneInfo);
        }
        catch
        {
            return localDateTime;
        }
    }

    public static DateTime? ToUtc(this DateTime? localDateTime, string timeZone)
    {
        return localDateTime is null ? null : localDateTime.Value.ToUtc(timeZone);
    }

    public static DateTime ToTimeZone(this DateTime utcDateTime, string timeZone)
    {
        try
        {
            TimeZoneInfo timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(timeZone);
            return TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, timeZoneInfo);
        }
        catch
        {
            return utcDateTime;
        }
    }

    public static DateTime? ToTimeZone(this DateTime? utcDateTime, string timeZone)
    {
        return utcDateTime is null ? null : utcDateTime.Value.ToTimeZone(timeZone);
    }

    public static string ToTimeZoneString(this DateTime utcDateTime, string timeZone, bool hasSeconds = true)
    {
        try
        {
            return utcDateTime.ToTimeZone(timeZone)
                .ToString($"dd-MMM-yyyy hh:mm{(hasSeconds ? ":ss" : string.Empty)} tt");
        }
        catch
        {
            return string.Empty;
        }
    }

    public static string ToTimeZoneString(this DateTime? utcDateTime, string timeZone, bool hasSeconds = true)
    {
        return utcDateTime is null ? string.Empty : utcDateTime.Value.ToTimeZoneString(timeZone, hasSeconds);
    }

    public static string ToPrettyString(this DateTime dateTime, bool ifIsDateHasTime = false)
    {
        string result = string.Empty;
        try
        {
            result = (int)(DateTime.Now - dateTime).TotalMinutes switch
            {
                > 1440 => dateTime.ToString($"dd-MMM-yyyy{(ifIsDateHasTime ? " HH:mm:ss" : string.Empty)}"),
                var i and > 60 => i / 60 + " hours ago",
                60 => "1 hour ago",
                var i and > 1 => i + " minutes ago",
                1 => "1 minute ago",
                0 => "now",
                _ => dateTime.ToString("dd-MM-yyyy")
            };
        }
        catch
        {
            // ignored
        }

        return result;
    }
}