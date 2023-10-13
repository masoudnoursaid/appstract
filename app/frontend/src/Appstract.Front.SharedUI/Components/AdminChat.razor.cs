using Appstract.Front.Application.Services;
using Appstract.Front.Domain.Models.ApiResponseModels;
using Appstract.Front.Domain.Models.Profile;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;
using MudBlazor;

namespace Appstract.Front.SharedUI.Components;

public partial class AdminChat
{
    private string Name { get; set; } = string.Empty;
    private string Email { get; set; } = string.Empty;

    [Inject] 
    private IJSRuntime JsRuntime { get; set; } = null!;

    [Inject] 
    private IProfileService ProfileService { get; set; } = null!;
    
    [Inject]
    private ILogger Logger { get; set; } = null!;

    private Variant _variant;
    private Color _color;
    private string _class;

    protected override async Task OnInitializedAsync()
    {
        ChatOff();
        try
        {
            ApiResponseBase<ProfileInfoResponse> user = await ProfileService.GetProfileSettingAsync();
            if (user.Success)
            {
                Name = user.Data.FirstName + " " + user.Data.LastName;
                Email = user.Data.Email;
            }
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error in loading admin chat");
        }
    }

    private void ChatOff()
    {
        _class = "admin-chat-paper--white";
        _color = Color.Default;
        _variant = Variant.Text;
    }

    private void ChatOn()
    {
        _class = "admin-chat-paper--purple";
        _color = Color.Primary;
        _variant = Variant.Filled;
    }
    private async Task OpenChat()
    {
        ChatOn();
        await JsRuntime.InvokeVoidAsync("OpenChat", Name, Email);
    }
}
