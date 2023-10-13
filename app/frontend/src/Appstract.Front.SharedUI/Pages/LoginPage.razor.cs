using Appstract.Front.Application.Common.Constants;
using Microsoft.AspNetCore.Components;

namespace Appstract.Front.SharedUI.Pages;

public partial class LoginPage
{
    private const string ROUTE = Routes.LOGIN;

    [Inject]
    protected NavigationManager NavigationManager { get; set; } = null!;

    protected override void OnInitialized()
    {
        NavigationManager.NavigateTo($"{Routes.DASHBOARD}");
    }
}