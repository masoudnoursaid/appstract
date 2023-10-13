using Application.Common.ResponseModel;

namespace Application.Services.Payment.Stripe.ResponseModel;

public class VerifyStripePaymentResponseModel : VerifyPaymentResponseModel
{
    public VerifyStripePaymentResponseModel(string payerProvidedId, bool verified) : base(payerProvidedId, verified)
    {
    }
}