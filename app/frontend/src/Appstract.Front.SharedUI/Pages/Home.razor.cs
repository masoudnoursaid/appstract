using Appstract.Front.Application.Common.Constants;
using Microsoft.AspNetCore.Components;

namespace Appstract.Front.SharedUI.Pages;

public partial class Home
{
    [Inject]
    private NavigationManager NavManager { get; set; }= default!;
    
    protected override void OnInitialized()
    {
        RedirectToLoginPage();
    }

    private void RedirectToLoginPage(){
        NavManager.NavigateTo(Routes.LOGIN);
    }
}