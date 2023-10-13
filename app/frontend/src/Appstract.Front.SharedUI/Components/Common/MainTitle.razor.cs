using Microsoft.AspNetCore.Components;

namespace Appstract.Front.SharedUI.Components.Common;

public partial class MainTitle
{
    [Parameter]
    [EditorRequired]
    public string Title { get; set; } = string.Empty;
    [Parameter]
    [EditorRequired]
    public string Class { get; set; } = string.Empty;
}