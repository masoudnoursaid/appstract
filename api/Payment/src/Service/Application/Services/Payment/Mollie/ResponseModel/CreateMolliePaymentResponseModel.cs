using Application.Common.ResponseModel;

namespace Application.Services.Payment.Mollie.ResponseModel;

public sealed class CreateMolliePaymentResponseModel : CreatePaymentResponseModel
{
    public CreateMolliePaymentResponseModel(string url, string providedId)
    {
        Url = url;
        ProvidedId = providedId;
    }
}