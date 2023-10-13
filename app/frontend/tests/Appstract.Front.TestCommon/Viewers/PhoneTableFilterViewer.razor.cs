using Appstract.Front.Domain.Enums;
using Microsoft.AspNetCore.Components;

namespace Appstract.TestCommon.Viewers;

public partial class PhoneTableFilterViewer
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
}