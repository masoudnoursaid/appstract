namespace Appstract.Mobile.AcceptanceTests.Extensions;

public static class EnumExtensions
{
    public static T Next<T>(this T enumValue) where T : Enum
    {
        T[] values = (T[])Enum.GetValues(typeof(T));
        int currentIndex = Array.IndexOf(values, enumValue);
        int nextIndex = (currentIndex + 1) % values.Length;
        return values[nextIndex];
    }
    public static T Previous<T>(this T enumValue) where T : Enum
    {
        T[] values = (T[])Enum.GetValues(typeof(T));
        int currentIndex = Array.IndexOf(values, enumValue);
        int previousIndex = (currentIndex - 1 + values.Length) % values.Length;
        return values[previousIndex];
    }

    public static void CheckIsDefined<T>(this T enumValue) where T : Enum
    {
        if (!Enum.IsDefined(typeof(T), enumValue))
        {
            throw new ArgumentException($"The value {enumValue} is not defined for the enum {typeof(T).Name}");
        }
    }
}