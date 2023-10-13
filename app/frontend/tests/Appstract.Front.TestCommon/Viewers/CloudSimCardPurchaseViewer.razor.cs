using Appstract.Front.Domain.Models;
using Appstract.Front.Domain.Models.ApiResponseModels;
using Appstract.Front.Domain.Models.CloudSimCard;
using Microsoft.AspNetCore.Components;

namespace Appstract.TestCommon.Viewers;

public partial class CloudSimCardPurchaseViewer
{
    [Parameter]
    public Func<CloudSimCardCountryCarrierRequestFilter,
            Task<ApiResponseBase<PaginatedResponse<CloudSimCardCountryCarrierModel>>>>
        GetCloudSimCardCountryCarriers { get; set; } = null!;
    
    [Parameter]
    public Func<CloudSimCardMobileNumberRequestFilter,
            Task<ApiResponseBase<PaginatedResponse<CloudSimCardMobileNumberModel>>>>
        GetCloudSimCardMobileNumbers { get; set; } = null!;

    [Parameter]
    public Func<List<string>,
            Task<ApiResponseBase<List<CloudSimCardAvailableCarriersModel>>>>
        GetCloudSimCardAvailableCarriers { get; set; } = null!;
    
    [Parameter]
    public Func<Task<ApiResponseBase<CloudSimCardPortPriceInfoModel>>>
        GetCloudSimCardPortPriceInfo { get; set; } = null!;
    
    [Parameter]
    public Func<Task<ApiResponseBase<UserWalletInfo>>> GetUserWalletInfo { get; set; } = null!;
}