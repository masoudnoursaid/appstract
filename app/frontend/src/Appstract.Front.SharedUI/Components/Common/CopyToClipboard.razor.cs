using Appstract.Front.Application.Common.Constants;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor;

namespace Appstract.Front.SharedUI.Components.Common;

public partial class CopyToClipboard
{
    [Parameter]
    public string? Text { get; set; }

    [Inject]
    public ISnackbar SnackbarService { get; set; } = null!;
    [Inject]
    public IJSRuntime JsRuntime { get; set; } = null!;

    private async Task Copy()
    {
        await JsRuntime.InvokeVoidAsync("navigator.clipboard.writeText", Text ?? string.Empty);

        SnackbarService.Configuration.PositionClass = Defaults.Classes.Position.TopCenter;
        SnackbarService.Configuration.ShowCloseIcon = false;
        SnackbarService.Configuration.ShowTransitionDuration = 500;
        SnackbarService.Configuration.VisibleStateDuration = 1500;
        SnackbarService.Configuration.HideTransitionDuration = 500;
        SnackbarService.Add("Content copied to clipboard", Severity.Normal, config =>
            {
                config.Icon = UltraToneIcons.CONTENT_COPY;
                config.ShowCloseIcon = true;
            }
        );
    }
}