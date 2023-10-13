using Microsoft.AspNetCore.Components;

namespace Appstract.TestCommon.Viewers;

public partial class CopyToClipboardViewer
{
    [Parameter] 
    public string? Text { get; set; }
}