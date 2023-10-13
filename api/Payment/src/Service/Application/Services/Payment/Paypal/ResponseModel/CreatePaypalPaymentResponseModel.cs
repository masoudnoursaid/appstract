using Application.Common.ResponseModel;

namespace Application.Services.Payment.Paypal.ResponseModel;

public sealed class CreatePaypalPaymentResponseModel : CreatePaymentResponseModel
{
    public CreatePaypalPaymentResponseModel(string url, string providedId)
    {
        Url = url;
        ProvidedId = providedId;
    }
}