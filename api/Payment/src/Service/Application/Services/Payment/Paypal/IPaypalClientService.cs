using Application.Services.Payment.Paypal.Payloads;
using Application.Services.Payment.Paypal.ResponseModel;

namespace Application.Services.Payment.Paypal;

public interface IPaypalClientService : IPaymentGetWayService, IThirdPartyService,
    IPaymentUrlGenerator<CreatePaypalPaymentResponseModel, CreatePaypalPaymentPayload>,
    IPaymentUrlVerifier<VerifyPaypalPaymentResponseModel, VerifyPaypalPaymentPayload>
{
}