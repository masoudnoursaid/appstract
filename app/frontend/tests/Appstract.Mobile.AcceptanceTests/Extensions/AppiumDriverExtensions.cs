using OpenQA.Selenium.Appium;

namespace Appstract.Mobile.AcceptanceTests.Extensions;

public static class AppiumDriverExtensions
{
    public static T OnPlatform<T>(this AppiumDriver<AppiumWebElement> driver, T ios, T android, T windows)
    {
        return driver.PlatformName switch
        {
            "iOS" => ios,
            "Android" => android,
            "Windows" => windows,
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}