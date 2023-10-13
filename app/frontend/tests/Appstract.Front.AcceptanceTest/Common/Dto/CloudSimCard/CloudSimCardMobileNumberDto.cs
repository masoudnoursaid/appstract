namespace Appstract.AcceptanceTest.Common.Dto.CloudSimCard;

public class CloudSimCardMobileNumberDto
{
    public string Country { get; set; } = string.Empty;
    public string Carrier { get; set; } = string.Empty;
    public string MobileNumber { get; set; } = string.Empty;
    public string SimSetupFee { get; set; } = string.Empty;
    public string MonthlyFee { get; set; } = string.Empty;
    public string DomesticSmsFee { get; set; } = string.Empty;
    public string DomesticCallFee { get; set; } = string.Empty;
}