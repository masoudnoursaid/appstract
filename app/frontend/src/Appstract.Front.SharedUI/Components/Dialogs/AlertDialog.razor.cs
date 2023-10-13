using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Appstract.Front.SharedUI.Components.Dialogs;

public partial class AlertDialog
{
    [CascadingParameter] 
    private MudDialogInstance MudDialog { get; set; } = null!;

    [Parameter] 
    public string IconDialog { get; set; } = Icons.Material.Filled.Info;
    
    [Parameter]
    public Color IconColor { get; set; } = Color.Info;

    [Parameter] 
    public string Title { get; set; } = string.Empty;
    
    [Parameter] 
    public Color TitleColor { get; set; } = Color.Info;

    [Parameter]
    public string Message { get; set; } = string.Empty;

    [Parameter]
    public string OkText { get; set; } = "OK";

    [Parameter]
    public bool ShowCancel { get; set; }

    private void OnOkClick()
    {
        MudDialog.Close(DialogResult.Ok(true));
    }

    private void OnCancelClick()
    {
        MudDialog.Cancel();
    }
}