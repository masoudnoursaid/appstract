using Appstract.Front.Domain.Models.ApiRequestModels;

namespace Appstract.Front.Domain.Models.PurchaseHistory;

public class PurchaseHistoryRequestFilter : Pagination
{
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
    public List<string> Status { get; set; } = new();
}