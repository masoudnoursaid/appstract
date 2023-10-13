using Microsoft.AspNetCore.Components;

namespace Appstract.Front.SharedUI.Components.Common;

public partial class TablePagination
{
    [Parameter]
    public string Class { get; set; } = string.Empty;

    [Parameter]
    public int PagesCount { get; set; }

    [Parameter]
    public int SelectedPage { get; set; }

    [Parameter]
    public EventCallback<int> SelectedPageChanged { get; set; }
}