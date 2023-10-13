namespace Appstract.Front.Domain.Models.CloudSimCard;

public class CloudSimCardCountryCarrierModel
{
    public string CountryCode { get; set; } = string.Empty;
    public string Carrier { get; set; } = string.Empty;
    public decimal MonthlyFee { get; set; }
    public decimal DomesticSmsFee { get; set; }
    public decimal DomesticCallFee { get; set; }
    public string Currency { get; set; } = string.Empty;
    public string NumberRange { get; set; } = string.Empty;
}