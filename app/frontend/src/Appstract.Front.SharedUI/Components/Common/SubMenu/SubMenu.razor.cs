using Microsoft.AspNetCore.Components;

namespace Appstract.Front.SharedUI.Components.Common.SubMenu;

public partial class SubMenu
{
    [Parameter]
    [EditorRequired]
    public string Class { get; set; } = string.Empty;
    
    [Parameter]
    [EditorRequired]
    public string Title { get; set; } = string.Empty;
    
    [Parameter]
    [EditorRequired]
    public string Typo { get; set; } = string.Empty;
    
    [Parameter]
    public RenderFragment ChildContent { get; set; } = null!;
    private bool _isTransformed = false;

    [Parameter]
    public EventCallback<bool> CloseMenuClicked { get; set; }

    private void CloseSubMenu()
    {
        _isTransformed = true;
        CloseMenuClicked.InvokeAsync();

    }
}