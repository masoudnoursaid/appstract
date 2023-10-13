namespace Appstract.Front.Domain.Models.PurchaseHistory;

public class PurchaseHistoryModel
{
    public DateTime Date { get; set; }
    public string TransactionId { get; set; } = string.Empty;
    public string ExternalReference { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public decimal? BalanceBefore { get; set; }
    public decimal? BalanceAfter { get; set; }
    public string Currency { get; set; } = string.Empty;
    public string LogoPath { get; set; } = string.Empty;
    public string PaymentMethod { get; set; } = string.Empty;
}