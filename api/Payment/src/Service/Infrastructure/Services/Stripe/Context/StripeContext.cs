using Application.Common.BaseTypes.Context;
using Stripe;

namespace Infrastructure.Services.Stripe.Context;

public class StripeContext : IPaymentContext
{
    public StripeContext(string apiKey)
    {
        RequestOptions = new RequestOptions
        {
            ApiKey = apiKey
        };
    }

    public RequestOptions RequestOptions { get; private set; }
}