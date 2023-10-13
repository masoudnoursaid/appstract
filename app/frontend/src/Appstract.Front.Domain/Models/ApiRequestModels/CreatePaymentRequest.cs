namespace Appstract.Front.Domain.Models.Financial;

public class CreatePaymentRequest
{
    public PaymentMethod PaymentMethod { get; set; } = new();
    public decimal Amount { get; set; }
}