using Application.Common.BaseTypes.Dto;

namespace Application.Business.Payment.Commands.CreatePayment;

public record CreatePaymentDto
    (string PaymentUrl, string ProvidedId, string PaymentId) : IBaseDto;