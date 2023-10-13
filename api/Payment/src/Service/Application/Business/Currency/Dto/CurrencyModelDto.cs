using Application.Common.BaseTypes.Dto;

namespace Application.Business.Currency.Dto;

public record CurrencyModelDto : IBaseDto
{
    public string Title { get; set; } = null!;

    public string FullName { get; set; } = null!;

    public string Symbol { get; set; } = null!;
}