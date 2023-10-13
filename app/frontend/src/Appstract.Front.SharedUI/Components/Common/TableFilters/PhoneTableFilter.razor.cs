using Appstract.Front.Domain.Enums;
using Microsoft.AspNetCore.Components;

namespace Appstract.Front.SharedUI.Components.Common.TableFilters;

public partial class PhoneTableFilter
{
    [Parameter] 
    public FilterMatchType PhoneNumberQueryType { get; set; }
    [Parameter] 
    public EventCallback<FilterMatchType> PhoneNumberQueryTypeChanged { get; set; }
    [Parameter] 
    public string PhoneNumber { get; set; } = string.Empty;
    [Parameter] 
    public EventCallback<string> PhoneNumberChanged { get; set; }
    [Parameter] 
    public Action ReloadTable { get; set; } = null!;
    [Parameter] 
    public string Class { get; set; } = string.Empty;
    [Parameter] 
    public string Title { get; set; } = string.Empty;

    private bool _isPhoneFilterOpen;
    private string _tempPhoneNumber = string.Empty;

    private void Clear()
    {
        _tempPhoneNumber = string.Empty;
        Ok();
    }
    
    private void Ok()
    {
        PhoneNumber = _tempPhoneNumber;
        PhoneNumberQueryTypeChanged.InvokeAsync(PhoneNumberQueryType);
        PhoneNumberChanged.InvokeAsync(PhoneNumber);
        ReloadTable.Invoke(); 
        _isPhoneFilterOpen = false;
    }
    
    private void Cancel()
    {
        _tempPhoneNumber = PhoneNumber;
        _isPhoneFilterOpen = false;
    }
}