using Appstract.Front.Domain.Enums;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Appstract.Front.SharedUI.Components.Dialogs;

public partial class MessageDialog
{
    [CascadingParameter] 
    private MudDialogInstance MudDialog { get; set; } = null!;
    
    [Parameter]
    public string Class { get; set; } = string.Empty;
    [Parameter]
    public MessageDialogType Type { get; set; } = MessageDialogType.Info; 
    [Parameter]
    public string Icon { get; set; } = string.Empty;
    [Parameter] 
    public string Title { get; set; } = string.Empty;
    [Parameter]
    public string Description { get; set; } = string.Empty;
    [Parameter]
    public string Subtitle { get; set; } = string.Empty;
    [Parameter]
    public string CancelButtonText { get; set; } = "Cancel";
    [Parameter]
    public string OkButtonText { get; set; } = "Ok";

    private void OnCancelClick()
    {
        MudDialog.Cancel();
    }

    public void OnOkClick()
    {
        MudDialog.Close(DialogResult.Ok(true));
    }

    private Color IconColor
    {
        get
        {
            switch (Type)
            {
                case MessageDialogType.Success:
                    return Color.Success;
                case MessageDialogType.Error:
                    return Color.Error;
                case MessageDialogType.Info:
                    return Color.Info;
                case MessageDialogType.Warning:
                    return Color.Warning;
                default:
                    return Color.Primary;
            }
            
        }
    }
}