using Appstract.Front.Application.Common.Constants;
using Microsoft.AspNetCore.Components;

namespace Appstract.Front.SharedUI.Components;

public partial class NotFoundPage
{
    [Inject]
    private NavigationManager NavigationManager { get; set; } = null!;

    private void ReturnHome()
    {
        NavigationManager.NavigateTo(Routes.DASHBOARD, true);
    }
}