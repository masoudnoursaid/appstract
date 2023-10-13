using Appstract.Front.Domain.Enums;
using Appstract.Front.Domain.Models.ApiRequestModels;

namespace Appstract.Front.Domain.Models.CloudSimCard;

public class CloudSimCardMobileNumberRequestFilter : Pagination
{
    public bool IsOriginalCurrency { get; set; }
    public List<string> CountriesFilter { get; set; } = new();
    public List<string> CarriersFilter { get; set; } = new();
    public FilterMatchType MobileNumberFilterType { get; set; } = FilterMatchType.Exact;
    public string MobileNumberFilter { get; set; } = string.Empty;
}