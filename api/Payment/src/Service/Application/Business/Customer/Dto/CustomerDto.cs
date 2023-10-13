using Application.Common.BaseTypes.Dto;

namespace Application.Business.Customer.Dto;

public record CustomerDto : IBaseDto
{
    public string Name { get; set; } = null!;
    public string Mobile { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PaymentId { get; set; } = null!;
}