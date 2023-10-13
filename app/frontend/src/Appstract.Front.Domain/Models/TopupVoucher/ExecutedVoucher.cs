namespace Appstract.Front.Domain.Models.TopupVoucher;

public class ExecutedVoucher
{
    public string Serial { get; set; } = string.Empty;
    public decimal Credit { get; set; } 
    public string Currency { get; set; } = string.Empty;
    public DateTime UsedDate { get; set; }
}
