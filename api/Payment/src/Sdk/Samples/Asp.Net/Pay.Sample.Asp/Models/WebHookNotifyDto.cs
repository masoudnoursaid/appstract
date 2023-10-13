namespace Pay.Sample.Asp.Models;

public record WebHookNotifyDto(string PaymentId, string ProvidedId, bool Successful, string Status,
    string Amount, string InvoiceNumber);