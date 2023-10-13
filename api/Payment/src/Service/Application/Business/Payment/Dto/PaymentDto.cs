using Application.Business.Currency.Dto;
using Application.Business.PaymentStatus.Dto;
using Application.Common.BaseTypes.Dto;

namespace Application.Business.Payment.Dto;

public record PaymentDto : IDto
{
    public string? ProvidedId { get; set; }
    public float Amount { get; set; }
    public string ClientRedirectUrl { get; set; } = null!;


    public string PaymentMethodId { get; set; } = null!;
    public string PaymentMethodDisplayTitle { get; set; } = null!;

    public string? PayerId { get; set; }


    public PaymentStatusDto Status { get; set; } = null!;


    public CurrencyModelDto Currency { get; set; } = null!;


    public string? TransactionId { get; set; }

    public string? CustomerId { get; set; }
    public string Id { get; set; } = null!;
    public DateTime CreatedDateTime { get; set; }
    public DateTime UpdatedDateTime { get; set; }
}