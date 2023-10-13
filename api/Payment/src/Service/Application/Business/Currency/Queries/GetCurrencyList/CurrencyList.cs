using Application.Business.Currency.Dto;

namespace Application.Business.Currency.Queries.GetCurrencyList;

public record CurrencyList(IEnumerable<CurrencyDto> Dto);