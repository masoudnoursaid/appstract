namespace Payment.Api.Integration.Billplz.Context;

public class BillplzTestFixtureContext : IBillplzTestFixtureContext
{
    public string MethodId { get; private set; } = null!;

    public void SetMethodId(string paymentMethodId)
    {
        if (string.IsNullOrEmpty(paymentMethodId))
            throw new ArgumentNullException(paymentMethodId);
        MethodId = paymentMethodId;
    }
}