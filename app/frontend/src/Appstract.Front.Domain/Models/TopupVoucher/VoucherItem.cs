namespace Appstract.Front.Domain.Models.TopupVoucher;

public class VoucherItem
{
    public int Id { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime ExpirationDate { get; set; }
    public string Voucher { get; set; } = string.Empty;
    public string Currency { get; set; } = string.Empty;
    public decimal Credit { get; set; }
    public bool Activated { get; set; }
    public bool Used { get; set; }
}