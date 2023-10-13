using Appstract.Front.Domain.Enums;

namespace Appstract.Front.Domain.Models.PurchaseHistory;

public class PurchaseHistoryDetailsModel
{
    public PaymentStatusType PaymentStatusName { get; set; }
    public decimal PaymentAmountInOrderCurrency { get; set; }
    public decimal PaymentAmountInBaseCurrency { get; set; }
    public decimal ExchangeRate { get; set; }
    public string PaymentCurrency { get; set; } = string.Empty;
    public string PaymentMethodName { get; set; } = string.Empty;
    public string TrxKey { get; set; } = string.Empty;
    public string ReferenceId { get; set; } = string.Empty;
    public decimal BalanceBefore { get; set; }
    public decimal BalanceAfter { get; set; }
    public string BaseCurrency { get; set; } = string.Empty;
}