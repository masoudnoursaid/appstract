using Application.Common.Payloads;
using Domain.ValueObjects.Payment;

namespace Application.Services.Payment.Paypal.Payloads;

public record CreatePaypalPaymentPayload(string ClientRedirectUrl, IEnumerable<PaymentItem> Items, float Amount,
    string Currency, string InvoiceNumber, string Description, string Email, string Mobile, string Name,
    string ClientWebHookUrl) : CreatePaymentPayload(ClientRedirectUrl, Items, Amount, Currency, InvoiceNumber,
    Description, Email, Mobile, Name, ClientWebHookUrl)
{
    public string PaymentId { get; set; } = null!;
}