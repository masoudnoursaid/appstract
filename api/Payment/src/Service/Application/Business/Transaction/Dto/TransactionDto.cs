using Domain.ValueObjects.Payment;

namespace Application.Business.Transaction.Dto;

public record TransactionDto
{
    public string InvoiceNumber { get; set; } = null!;
    public bool Successful { get; set; } = false;
    public string? Description { get; set; }
    public IEnumerable<PaymentItem> Items { get; set; } = null!;
    
}