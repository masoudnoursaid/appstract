using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Appstract.TestCommon.Viewers;

public partial class DateRangeTableFilterViewer
{
    [Parameter] 
    public Action ReloadTable { get; set; } = null!;
    [Parameter] 
    public string Class { get; set; } = string.Empty;
    [Parameter] 
    public DateRange? Dates { get; set; }
    [Parameter] 
    public EventCallback<DateRange?> DatesChanged { get; set; }
}