using Application.Common.ResponseModel;

namespace Application.Services.Payment.BillPlz.ResponseModel;

public sealed class CreateBillPlzPaymentResponseModel : CreatePaymentResponseModel
{
    public CreateBillPlzPaymentResponseModel(string url, string providedId)
    {
        Url = url;
        ProvidedId = providedId;
    }
}

public class VerifyBillPlzPaymentResponseModel : VerifyPaymentResponseModel
{
    public VerifyBillPlzPaymentResponseModel(string payerProvidedId, bool verified) : base(payerProvidedId, verified)
    {
    }
}