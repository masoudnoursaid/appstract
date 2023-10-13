using Domain.Common.BaseTypes;
using Domain.Enums;

namespace Domain.Entities;

public class PaymentStatus : BaseEntity
{
    public PaymentProcessType? ProcessStatus { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
}