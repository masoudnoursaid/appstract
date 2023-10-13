using Application.Services.Payment.Stripe.Payloads;
using Application.Services.Payment.Stripe.ResponseModel;

namespace Application.Services.Payment.Stripe;

public interface IStripeClientService : IPaymentGetWayService, IThirdPartyService,
    IPaymentUrlGenerator<CreateStripePaymentResponseModel, CreateStripePaymentPayload>,
    IPaymentUrlVerifier<VerifyStripePaymentResponseModel, VerifyStripePaymentPayload>
{
}