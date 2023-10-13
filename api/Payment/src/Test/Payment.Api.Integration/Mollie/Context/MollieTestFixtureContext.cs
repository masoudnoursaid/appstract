namespace Payment.Api.Integration.Mollie.Context;

public class MollieTestFixtureContext : IMollieTestFixtureContext
{
    public string MethodId { get; private set; } = null!;

    public void SetMethodId(string paymentMethodId)
    {
        if (string.IsNullOrEmpty(paymentMethodId))
            throw new ArgumentNullException(paymentMethodId);
        MethodId = paymentMethodId;
    }
}