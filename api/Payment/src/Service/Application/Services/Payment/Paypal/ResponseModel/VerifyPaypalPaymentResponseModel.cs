using Application.Common.ResponseModel;

namespace Application.Services.Payment.Paypal.ResponseModel;

public class VerifyPaypalPaymentResponseModel : VerifyPaymentResponseModel
{
    public VerifyPaypalPaymentResponseModel(string payerProvidedId, bool verified) : base(payerProvidedId, verified)
    {
    }
}