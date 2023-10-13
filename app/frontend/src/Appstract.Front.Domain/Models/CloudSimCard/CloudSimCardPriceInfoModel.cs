namespace Appstract.Front.Domain.Models.CloudSimCard;

public class CloudSimCardPriceInfoModel
{
    public string CountryCode { get; set; } = string.Empty;
    public string MobileNumber { get; set; } = string.Empty;
    public decimal MonthlyFee { get; set; }
    public decimal SimSetupFee { get; set; }
    public decimal PortSetupFee { get; set; }
    public bool IsDedicated { get; set; }
    public decimal TotalPrice => (12 * MonthlyFee) + SimSetupFee + (IsDedicated ? PortSetupFee : 0);
    public string Currency { get; set; } = string.Empty;
}