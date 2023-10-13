using Application.Business.PaymentMethod.Dto;

namespace Application.Business.PaymentMethod.Queries.GetPaymentMethodList;

public record PaymentMethodList(IEnumerable<PaymentMethodDto> Dtos);