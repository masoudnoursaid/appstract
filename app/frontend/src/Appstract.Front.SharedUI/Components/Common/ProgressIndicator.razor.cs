using Microsoft.AspNetCore.Components;

namespace Appstract.Front.SharedUI.Components.Common;

public partial class ProgressIndicator
{
    [Parameter]
    public List<string> StepTitles { get; set; } = null!;
        
    [Parameter]
    public int ActiveIndex { get; set; }

    [Parameter]
    public string Class { get; set; } = string.Empty;
}