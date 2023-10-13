namespace Payment.Api.Integration.Paypal.Context;

public class PaypalTestFixtureContext : IPaypalTestFixtureContext
{
    public string MethodId { get; private set; } = null!;

    public void SetMethodId(string paymentMethodId)
    {
        if (string.IsNullOrEmpty(paymentMethodId))
            throw new ArgumentNullException(paymentMethodId);
        MethodId = paymentMethodId;
    }
}