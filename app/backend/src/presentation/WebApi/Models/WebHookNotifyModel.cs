namespace Appstract.WebApi.Models;

public record WebHookNotifyModel(string PaymentId, string ProvidedId, bool Successful, string Status,
    string Amount, string InvoiceNumber);