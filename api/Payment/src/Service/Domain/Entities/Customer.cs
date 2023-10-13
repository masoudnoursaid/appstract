using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common.BaseTypes;

namespace Domain.Entities;

public class Customer : BaseEntity
{
    public string Name { get; set; } = null!;
    public string Mobile { get; set; } = null!;
    public string Email { get; set; } = null!;

    [ForeignKey(nameof(Payment))] public string PaymentId { get; set; } = null!;
    public Payment Payment { get; set; } = null!;
}