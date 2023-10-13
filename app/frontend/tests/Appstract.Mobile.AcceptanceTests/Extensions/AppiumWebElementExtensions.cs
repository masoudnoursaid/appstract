using OpenQA.Selenium.Appium;

namespace Appstract.Mobile.AcceptanceTests.Extensions;

public static class AppiumWebElementExtensions
{
    public static void TypeText(this AppiumWebElement element, string text)
    {
        element.Click();
        element.Clear();
        element.SendKeys(text);
    }
}