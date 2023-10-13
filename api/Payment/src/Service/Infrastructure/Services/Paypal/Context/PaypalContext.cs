using Application.Common.BaseTypes.Context;
using PayPal.Api;

namespace Infrastructure.Services.Paypal.Context;

public class PaypalContext : APIContext, IPaymentContext
{
    public PaypalContext(string token) : base(token)
    {
    }
}