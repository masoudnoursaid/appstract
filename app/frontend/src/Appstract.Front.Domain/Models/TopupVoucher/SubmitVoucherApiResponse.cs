namespace Appstract.Front.Domain.Models.TopupVoucher;

public class SubmitVoucherApiResponse
{
    public string BaseCurrency { get; set; } = string.Empty;
    public decimal VoucherAmount { get; set; }
    public string VoucherCurrency { get; set; } = string.Empty;
    public decimal ExchangeRate { get; set; }
    public decimal BalanceAfterBaseCurrency { get; set; }
    public decimal BalanceBeforeBaseCurrency { get; set; }
    public decimal BalanceAfterVoucherCurrency { get; set; }
    public decimal BalanceBeforeVoucherCurrency { get; set; }
}