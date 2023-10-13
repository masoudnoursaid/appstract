using Appstract.Front.Application.Common.Constants;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor;

namespace Appstract.Front.SharedUI.Components.Dialogs;

public partial class ShareViaDialog
{
    [CascadingParameter] 
    private MudDialogInstance MudDialog { get; set; } = null!;

    [Parameter] 
    public string Content { get; set; } = string.Empty;

    [Inject] 
    public IJSRuntime JsRuntime { get; set; } = null!;
    [Inject] 
    public ISnackbar SnackbarService { get; set; } = null!;
    
    private void OnCloseClick() => MudDialog.Cancel();

    private string CreateShareableLink(string platform)
    {
        string encodedMessage = Uri.EscapeDataString(Content);
        return platform switch
        {
            "telegram" => $"https://t.me/share/url?url={encodedMessage}",
            "whatsapp" => $"https://wa.me/?text={encodedMessage}",
            "teams" => $"https://teams.microsoft.com/l/message/0/0?users=example@example.com&message={encodedMessage}",
            "twitter" => $"https://twitter.com/intent/tweet?text={encodedMessage}",
            "gmail" => $"mailto:?subject=Shared%20Message&body={encodedMessage}", 
            _ => string.Empty
        };
    }

    private async Task ShareMessage(string platform)
    {
        string url = CreateShareableLink(platform);
        await JsRuntime.InvokeVoidAsync("open", url, "_blank");
    }
    
    private async Task OnCopyClick()
    {
        await JsRuntime.InvokeVoidAsync("navigator.clipboard.writeText", Content);
        
        SnackbarService.Configuration.PositionClass = Defaults.Classes.Position.TopCenter;
        SnackbarService.Configuration.ShowCloseIcon = false;
        SnackbarService.Configuration.ShowTransitionDuration = 500;
        SnackbarService.Configuration.VisibleStateDuration = 1500;
        SnackbarService.Configuration.HideTransitionDuration = 500;
        SnackbarService.Add("Content copied to clipboard", Severity.Normal,config =>
        {
            config.Icon = UltraToneIcons.CONTENT_COPY;
            config.ShowCloseIcon = true;
        });
    }
}