using System.Linq;
using Appstract.Front.Application.Common.Constants;
using Appstract.Front.SharedUI.Components;
using Appstract.TestCommon.Base;
using Appstract.TestCommon.Viewers;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MudBlazor;

namespace Appstract.UnitTest.Components;

public class DashboardWrapperTest : ComponentTestContext
{
    private readonly IDialogService? _dialogService;

    public DashboardWrapperTest()
    {
        Services.AddScoped<IConfiguration, ConfigurationManager>();
        _dialogService = Services.GetService<IDialogService>();
    }

    [Fact]
    public void Appbar_RenderedSuccessful_HasProfileSettingLink()
    {
        FakeNavigationManager navigationManager = Services.GetRequiredService<FakeNavigationManager>();
        IRenderedComponent<DashboardWrapperViewer> cut = RenderComponent<DashboardWrapperViewer>();
        cut.InvokeAsync(() => cut.FindComponent<MudMenu>().Instance.OpenMenu(null));
        cut.WaitForElement(".mud-popover-open");

        cut.Find(".account__profile-setting").Click();

        navigationManager.Uri.Should().Be($"{navigationManager.BaseUri}{Routes.PERSONAL_INFO}");
    }

    [Fact]
    public void NavMenu_RenderedSuccessful_HasDashboardLink()
    {
        string dashboard = Routes.DASHBOARD.Replace("/", "\\/");

        IRenderedComponent<DashboardWrapper> cut = RenderComponent<DashboardWrapper>();

        cut.FindAll($"a[href={dashboard}]")
            .Should()
            .HaveCount(1);
    }

    [Fact]
    public void NavMenu_ClickOnDashboard_IconMusBeChangeToFilledIcon()
    {
        FakeNavigationManager navigationManager = Services.GetRequiredService<FakeNavigationManager>();
        IRenderedComponent<DashboardWrapper> cut = RenderComponent<DashboardWrapper>();
        
        navigationManager.NavigateTo(Routes.DASHBOARD);
        
        cut.FindComponents<MudIcon>()
            .Count(x => x.Instance.Icon == UltraToneIcons.DASHBOARD)
            .Should().Be(0);
        cut.FindComponents<MudIcon>()
            .Count(x => x.Instance.Icon == UltraToneIcons.FILLED_DASHBOARD)
            .Should().Be(1);
    }
}