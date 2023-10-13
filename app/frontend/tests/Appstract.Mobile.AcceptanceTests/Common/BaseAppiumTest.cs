using Appstract.Mobile.AcceptanceTests.Drivers;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using Xunit.Abstractions;

namespace Appstract.Mobile.AcceptanceTests.Common;

public abstract class BaseAppiumTest : IDisposable
{
    protected IAppiumDriver AppiumDriver { get; }
    private readonly ITestOutputHelper _testOutputHelper;

    protected PlatformType Platform { get; }

    protected BaseAppiumTest(IAppiumDriver appiumDriver,
        ITestOutputHelper testOutputHelper)
    {
        AppiumDriver = appiumDriver;
        _testOutputHelper = testOutputHelper;
    }

    protected T CreatePageModel<T>() where T : BasePageModel
    {
        T page = (T)Activator.CreateInstance(typeof(T), _testOutputHelper)!;
        page.LazyDriver = new Lazy<AppiumDriver<AppiumWebElement>>(() => AppiumDriver.Driver!);
        return page;
    }

    protected void LaunchApplication()
    {
        AppiumDriver.StartApp();
    }

    public void Dispose()
    {
        AppiumDriver.Dispose();
    }
}