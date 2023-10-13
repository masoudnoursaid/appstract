using Stripe;

namespace Infrastructure.Services.Stripe.Extension;

public static class StripePaymentIntentExtension
{
    public static bool IsVerified(this PaymentIntent paymentIntent)
    {
        return paymentIntent.Status.Equals("succeeded", StringComparison.OrdinalIgnoreCase);
    }
}