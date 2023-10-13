using Appstract.Front.Domain.Models.ApiResponseModels;
using Appstract.Front.Domain.Models.CloudSimCard;
using Microsoft.AspNetCore.Components;

namespace Appstract.TestCommon.Viewers.CloudSimCard;

public partial class CloudSimCardMobileNumbersViewer
{
    [Parameter]
    public Func<CloudSimCardMobileNumberRequestFilter,
            Task<ApiResponseBase<PaginatedResponse<CloudSimCardMobileNumberModel>>>>
        GetCloudSimCardMobileNumbers { get; set; } = null!;

    [Parameter]
    public Func<List<string>, Task<ApiResponseBase<List<CloudSimCardAvailableCarriersModel>>>>
        GetCloudSimCardAvailableCarriers { get; set; } = null!;

    [Parameter]
    public CloudSimCardCountryCarrierModel SelectedCountryCarrier { get; set; } = null!;
}