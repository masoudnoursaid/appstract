using Domain.Common.BaseTypes;
using Domain.ValueObjects.Payment;

namespace Domain.Entities;

public class Transaction : BaseEntity
{
    public string InvoiceNumber { get; set; } = null!;
    public bool Successful { get; set; } = false;
    public string? Description { get; set; }
    public IEnumerable<PaymentItem> Items { get; set; } = new List<PaymentItem>();
}