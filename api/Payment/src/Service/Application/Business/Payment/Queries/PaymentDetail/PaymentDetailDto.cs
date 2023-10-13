using Application.Business.Currency.Dto;
using Application.Business.Customer.Dto;
using Application.Business.PaymentMethod.Dto;
using Application.Business.Transaction.Dto;
using Application.Common.BaseTypes.Dto;

namespace Application.Business.Payment.Queries.PaymentDetail;

public record PaymentDetailDto : IBaseDto
{
    public string? ProvidedId { get; set; }
    public float Amount { get; set; }
    public string ClientRedirectUrl { get; set; } = null!;
    public PaymentMethodDto? PaymentMethod { get; set; }
    public PayerEntity? Payer { get; set; }
    public PaymentStatusEntity Status { get; set; } = null!;
    public CurrencyDto Currency { get; set; } = null!;
    public TransactionDto? Transaction { get; set; }
    public CustomerDto? Customer { get; set; }
}