using Microsoft.AspNetCore.Components;

namespace Appstract.Front.SharedUI.Components;

public partial class ContentsWrapper
{
    [Parameter]
    [EditorRequired]
    public string Title { get; set; } = string.Empty;
        
    [Parameter]
    [EditorRequired]
    public string Class { get; set; } = string.Empty;

    [Parameter]
    public RenderFragment? ChildContent { get; set; }
}