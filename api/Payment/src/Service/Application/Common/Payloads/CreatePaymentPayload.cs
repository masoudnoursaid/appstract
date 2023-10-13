using Domain.ValueObjects.Payment;

namespace Application.Common.Payloads;

public record CreatePaymentPayload(string ClientRedirectUrl, IEnumerable<PaymentItem> Items, float Amount,
    string Currency, string InvoiceNumber, string Description, string Email, string Mobile, string Name,
    string ClientWebHookUrl) : IPayload;