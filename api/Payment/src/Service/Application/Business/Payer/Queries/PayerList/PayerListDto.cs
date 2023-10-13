using Application.Business.Payer.Dto;

namespace Application.Business.Payer.Queries.PayerList;

public record PayerListDto(IEnumerable<PayerDto> Dtos);