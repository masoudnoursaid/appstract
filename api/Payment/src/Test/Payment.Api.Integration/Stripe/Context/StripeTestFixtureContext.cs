namespace Payment.Api.Integration.Stripe.Context;

public class StripeTestFixtureContext : IStripeTestFixtureContext
{
    public string MethodId { get; private set; } = null!;

    public void SetMethodId(string paymentMethodId)
    {
        if (string.IsNullOrEmpty(paymentMethodId))
            throw new ArgumentNullException(paymentMethodId);
        MethodId = paymentMethodId;
    }
}