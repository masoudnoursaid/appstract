using Application.Services.Payment.BillPlz.Payloads;
using Application.Services.Payment.BillPlz.ResponseModel;

namespace Application.Services.Payment.BillPlz;

public interface IBillPlzClientService : IPaymentGetWayService, IThirdPartyService,
    IPaymentUrlGenerator<CreateBillPlzPaymentResponseModel, CreateBillPlzPaymentPayload>,
    IPaymentUrlVerifier<VerifyBillPlzPaymentResponseModel, VerifyBillPlzPaymentPayload>
{
}