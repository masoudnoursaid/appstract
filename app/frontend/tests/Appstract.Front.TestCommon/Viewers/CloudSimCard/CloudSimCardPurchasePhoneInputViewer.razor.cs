using Appstract.Front.Domain.Models.CloudSimCard;
using Microsoft.AspNetCore.Components;

namespace Appstract.TestCommon.Viewers.CloudSimCard;

public partial class CloudSimCardPurchasePhoneInputViewer
{
    [Parameter]
    public List<CloudSimCardMobileNumberModel> Items { get; set; } = new();

    [Parameter]
    public EventCallback<List<CloudSimCardMobileNumberModel>> ItemsChanged { get; set; }

    [Parameter]
    public Action OnNext { get; set; } = null!;
}