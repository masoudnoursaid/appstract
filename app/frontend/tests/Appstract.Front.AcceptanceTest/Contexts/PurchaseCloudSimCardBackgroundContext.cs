using System.Collections.Generic;
using Appstract.Front.Domain.Models;
using Appstract.Front.Domain.Models.CloudSimCard;

namespace Appstract.AcceptanceTest.Contexts;

public class PurchaseCloudSimCardBackgroundContext
{
    public List<CloudSimCardCountryCarrierModel> GetCloudSimCardCountryCarriersDelegateResponse { get; set; } = new();
    public List<CloudSimCardMobileNumberModel> GetCloudSimCardMobileNumbersDelegateResponse { get; set; } = new();
    public CloudSimCardPortPriceInfoModel GetCloudSimCardPortPriceInfoDelegateResponse { get; set; } = new();
    public UserWalletInfo GetUserWalletInfoDelegateResponse { get; set; } = new();
    public bool IsDelegateToGetCountryAndCarriersCalled { get; set; }
    public bool IsDelegateToGetPhoneNumbersCalled { get; set; }
    public bool IsDelegateToGetCloudSimCardPortPriceInfoCalled { get; set; }
    public bool IsDelegateToGetUserWalletInfoCalled { get; set; }
}