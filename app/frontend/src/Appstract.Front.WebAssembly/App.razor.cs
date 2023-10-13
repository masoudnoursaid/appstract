using Appstract.Front.Application.Services;
using Microsoft.AspNetCore.Components;

namespace Appstract.Front.Client;

public partial class App
{
    [Inject]
    private IErrorMessageService ErrorMessageService { get; set; } = null!;

    [Inject]
    private ILogger Logger { get; set; } = null!;

    protected override async Task OnInitializedAsync()
    {
        try
        {
            await ErrorMessageService.GetErrorMessageList();
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error in loading App.razor");
        }
    }
}