using Microsoft.AspNetCore.Components;

namespace Appstract.TestCommon.Viewers;

public partial class DashboardWrapperViewer
{
    [Parameter] 
    public string Class { get; set; } = string.Empty;
}