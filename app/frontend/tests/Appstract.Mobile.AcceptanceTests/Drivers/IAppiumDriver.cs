using OpenQA.Selenium.Appium;

namespace Appstract.Mobile.AcceptanceTests.Drivers;

public interface IAppiumDriver : IDisposable
{
    AppiumDriver<AppiumWebElement>? Driver { get; }
    void StartApp();
    void StopApp();
    void CloseApp();
}