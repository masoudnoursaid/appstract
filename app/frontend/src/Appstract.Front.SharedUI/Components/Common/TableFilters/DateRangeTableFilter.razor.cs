using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Appstract.Front.SharedUI.Components.Common.TableFilters;

public partial class DateRangeTableFilter
{
    [Parameter]
    public string Title { get; set; } = string.Empty;
    [Parameter] 
    public Action ReloadTable { get; set; } = null!;
    [Parameter] 
    public string Class { get; set; } = string.Empty;
    [Parameter] 
    public DateRange? Dates { get; set; }
    [Parameter] 
    public EventCallback<DateRange?> DatesChanged { get; set; }

    private DateRange? _tempDates;
    private bool _isDateFilterOpen;
    
    private void Ok()
    {
        Dates = _tempDates;
        DatesChanged.InvokeAsync(Dates);
        ReloadTable.Invoke();
        _isDateFilterOpen = false;
    }

    private void Clear()
    {
        _tempDates = null;
        Ok();
    }

    private void Cancel()
    {
        _tempDates = Dates;
        _isDateFilterOpen = false;
    }
}