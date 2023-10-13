using Application.Common.BaseTypes.Dto;
using Domain.Enums;

namespace Application.Business.PaymentStatus.Dto;

public record PaymentStatusDto : IBaseDto
{
    public PaymentProcessType? ProcessStatus { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
}