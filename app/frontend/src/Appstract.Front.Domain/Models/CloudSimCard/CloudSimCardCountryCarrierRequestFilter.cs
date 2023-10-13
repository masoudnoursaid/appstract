using Appstract.Front.Domain.Models.ApiRequestModels;

namespace Appstract.Front.Domain.Models.CloudSimCard;

public class CloudSimCardCountryCarrierRequestFilter : Pagination
{
    public bool IsOriginalCurrency { get; set; }
}