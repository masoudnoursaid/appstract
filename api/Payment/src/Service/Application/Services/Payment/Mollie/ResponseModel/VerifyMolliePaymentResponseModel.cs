using Application.Common.ResponseModel;

namespace Application.Services.Payment.Mollie.ResponseModel;

public class VerifyMolliePaymentResponseModel : VerifyPaymentResponseModel
{
    public VerifyMolliePaymentResponseModel(string payerProvidedId, bool verified) : base(payerProvidedId, verified)
    {
    }
}