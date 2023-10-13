using Appstract.Web.E2ETests.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Playwright;
using Xunit;

namespace Appstract.Web.E2ETests.Pages;

public abstract class BasePage : IClassFixture<UltraToneWebApplicationFactory<Program>>
{
    protected abstract string Path { get; }
    protected abstract IPage Page { get; set; }
    protected abstract IBrowser Browser { get; }

    private readonly string _baseUrl = "http://localhost:7280";

    public string GetBaseUrl()
    {
        return _baseUrl;
    }

    public string GetFullUrl()
    {
        return $"{_baseUrl}/{Path}";
    }

    protected BasePage(UltraToneWebApplicationFactory<Program> hostFactory)
    {
        hostFactory
            .WithWebHostBuilder(builder =>
            {
                builder.UseUrls(_baseUrl);
                builder.ConfigureServices(_ =>
                {
                });
                builder.ConfigureAppConfiguration((_, _) =>
                {
                });
            })
            .CreateDefaultClient();
    }

    public async Task NavigateAsync()
    {
        Page = await Browser.NewPageAsync();
        await Page.GotoAsync(GetFullUrl());
    }
}