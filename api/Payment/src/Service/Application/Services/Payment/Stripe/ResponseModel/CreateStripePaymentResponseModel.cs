using Application.Common.ResponseModel;

namespace Application.Services.Payment.Stripe.ResponseModel;

public sealed class CreateStripePaymentResponseModel : CreatePaymentResponseModel
{
    public CreateStripePaymentResponseModel(string url, string providedId)
    {
        Url = url;
        ProvidedId = providedId;
    }
}