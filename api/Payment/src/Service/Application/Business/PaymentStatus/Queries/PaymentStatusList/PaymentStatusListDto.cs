using Application.Business.PaymentStatus.Dto;

namespace Application.Business.PaymentStatus.Queries.PaymentStatusList;

public record PaymentStatusListDto(IEnumerable<PaymentStatusDto> Dtos);