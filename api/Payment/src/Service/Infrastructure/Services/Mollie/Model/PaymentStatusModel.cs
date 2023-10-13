namespace Infrastructure.Services.Mollie.Model;

public sealed record PaymentStatusModel(bool Verified, string PayerId);
