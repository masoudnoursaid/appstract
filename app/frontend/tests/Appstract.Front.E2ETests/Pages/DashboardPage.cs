using Appstract.Front.Application.Common.Constants;
using Appstract.Web.E2ETests.Infrastructure;
using Microsoft.Playwright;

namespace Appstract.Web.E2ETests.Pages;

public class DashboardPage : BasePage
{
    protected override string Path => $"{Routes.DASHBOARD}";
    protected override IPage Page { get; set; } = null!;
    protected override IBrowser Browser { get; }

    public DashboardPage(IBrowser browser) : base(new UltraToneWebApplicationFactory<Program>())
    {
        Browser = browser;
    }

    public async Task NavigateToLoginPage(string url)
    {
        Page = await Browser.NewPageAsync();
        await Page.GotoAsync(url);
    }

    public async Task FillAndSubmitLoginForm(string username, string password, string targetUrl)
    {
        await Page.FillAsync("input[name=username]", username);
        await Page.FillAsync("input[name=password]", password);

        await Page.RunAndWaitForNavigationAsync(
            async () => { await Page.ClickAsync(".login__submit-btn"); },
            new PageRunAndWaitForNavigationOptions { UrlString = targetUrl });
    }

    public async Task NavigateToDashboardPage()
    {
        await Page.GotoAsync(GetFullUrl());
    }

    public async Task<bool> ExistSpinnerModal()
    {
        IElementHandle? element = await Page.WaitForSelectorAsync(".loading-wrapper");
        return element is not null;
    }
}