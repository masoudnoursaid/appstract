using Application.Common.BaseTypes.Dto;

namespace Application.Business.Currency.Dto;

public record CurrencyDto : CurrencyModelDto, IDto
{
    public string Id { get; set; } = null!;
    public DateTime CreatedDateTime { get; set; }
    public DateTime UpdatedDateTime { get; set; }
}