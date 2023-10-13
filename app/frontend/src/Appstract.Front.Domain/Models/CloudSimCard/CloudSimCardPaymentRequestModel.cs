namespace Appstract.Front.Domain.Models.CloudSimCard;

public class CloudSimCardPaymentRequestModel
{
    public string MobileNumber { get; set; } = string.Empty;
    public bool IsDedicated { get; set; }
    public decimal Price { get; set; }
    public string Currency { get; set; } = string.Empty;
}