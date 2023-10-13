
using Microsoft.AspNetCore.Components;

namespace Appstract.Front.SharedUI.Components.Common;

public partial class Overlay 
{
    [Parameter] 
    public EventCallback OnOverlayClick { get; set; }

    private async Task HandleClick()
    {
        if (OnOverlayClick.HasDelegate)
        {
            await OnOverlayClick.InvokeAsync(null);
        }
    }
}