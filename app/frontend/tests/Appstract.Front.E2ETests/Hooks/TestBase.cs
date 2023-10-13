using Appstract.Web.E2ETests.Pages;
using BoDi;
using Microsoft.Playwright;
using Xunit;

[assembly: CollectionBehavior(MaxParallelThreads = 3)]

namespace Appstract.Web.E2ETests.Hooks;

[Binding]
public class TestBase
{
    [BeforeScenario("Dashboard")]
    public async Task BeforeLoginScenario(IObjectContainer container)
    {
        IPlaywright? playwright = await Playwright.CreateAsync();
        IBrowser browser = await playwright.Chromium.LaunchAsync();
        await browser.NewContextAsync(new BrowserNewContextOptions { IgnoreHTTPSErrors = true, AcceptDownloads = true });
        DashboardPage dashboardPage = new(browser);
        container.RegisterInstanceAs(playwright);
        container.RegisterInstanceAs(browser);
        container.RegisterInstanceAs(dashboardPage);
    }

    [AfterScenario]
    public async Task AfterScenario(IObjectContainer container)
    {
        IBrowser browser = container.Resolve<IBrowser>();
        await browser.CloseAsync();
        IPlaywright? playwright = container.Resolve<IPlaywright>();
        playwright.Dispose();
    }
}