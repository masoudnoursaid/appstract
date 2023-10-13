using Application.Business.Payment.Dto;

namespace Application.Business.Payment.Queries.PaymentList;

public record PaymentListDto(IEnumerable<PaymentDto> Dtos);