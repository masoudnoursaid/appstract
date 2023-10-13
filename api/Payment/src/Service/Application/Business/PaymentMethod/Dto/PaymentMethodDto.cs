using Application.Common.BaseTypes.Dto;

namespace Application.Business.PaymentMethod.Dto;

public record PaymentMethodDto : PaymentMethodModelDto, IDto
{
    public string Id { get; set; } = null!;
    public DateTime CreatedDateTime { get; set; }
    public DateTime UpdatedDateTime { get; set; }
}