using Application.Services.Payment.Mollie.Payloads;
using Application.Services.Payment.Mollie.ResponseModel;

namespace Application.Services.Payment.Mollie;

public interface IMollieClientService : IPaymentGetWayService, IThirdPartyService,
    IPaymentUrlGenerator<CreateMolliePaymentResponseModel, CreateMolliePaymentPayload>,
    IPaymentUrlVerifier<VerifyMolliePaymentResponseModel, VerifyMolliePaymentPayload>
{
}