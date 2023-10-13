using Microsoft.AspNetCore.Components;

namespace Appstract.Front.SharedUI.Components.Common;

public partial class UnknownError
{
    [Parameter]
    public EventCallback OnRetry { get; set; }

    private void OnRetryClick()
    {
        OnRetry.InvokeAsync();
    }
}