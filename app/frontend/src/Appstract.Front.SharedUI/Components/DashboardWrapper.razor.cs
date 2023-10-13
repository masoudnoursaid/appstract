using Appstract.Front.Application.Common.Resources;
using Appstract.Front.SharedUI.Components.Dialogs;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Appstract.Front.SharedUI.Components;

public partial class DashboardWrapper : LayoutComponentBase
{
    [Inject] 
    private NavigationManager NavigationManager { get; set; } = null!;
    
    [Inject] 
    private IDialogService DialogService { get; set; } = null!;

    [Parameter] 
    public string Class { get; set; } = string.Empty;
    
    [Parameter] 
    public List<BreadcrumbItem> BreadcrumbsItems { get; set; } = new();

    private bool IsNavLinkActive(string url)
    {
        return NavigationManager.Uri.Contains(url);
    }
    
    private Task OnLogoutClick()
    {
        return Task.CompletedTask;
    }
    
    private async Task OnLogoutTouch()
    {
        await Task.Delay(500);
        await OnLogoutClick();
    }
}