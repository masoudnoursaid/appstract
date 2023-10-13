namespace Appstract.Front.Domain.Models.ApiResponseModels;

public class CreatePaymentResponse
{
    public string CheckoutId { get; set; } = string.Empty;
    public string CheckoutUrl { get; set; } = string.Empty;
}