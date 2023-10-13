using Domain.Common.BaseTypes;

namespace Domain.ValueObjects.Payment;

public class PaymentItem : ValueObject
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Price { get; set; } = null!;
    public string Currency { get; set; } = null!;
    public int Quantity { get; set; }
    public string Sku { get; set; } = null!;

    public override string ToString()
    {
        return $"{Price} {Currency}";
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        throw new NotImplementedException();
    }
}