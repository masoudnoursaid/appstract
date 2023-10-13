using Microsoft.AspNetCore.Components;

namespace Appstract.TestCommon.Viewers;

public partial class EnumTableFilterViewer<TEnum> where TEnum : Enum
{
    [Parameter]
    public string Title { get; set; } = string.Empty;
    [Parameter] 
    public List<TEnum> SelectedValues { get; set; } = new();
    [Parameter] 
    public EventCallback<List<TEnum>> SelectedValuesChanged { get; set; }
    [Parameter] 
    public bool MultipleSelect { get; set; } = true;
    [Parameter] 
    public Action ReloadTable { get; set; } = null!;
    [Parameter] 
    public string Class { get; set; } = string.Empty;
}