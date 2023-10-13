using Application.Common.BaseTypes.Model;

namespace Application.Common.ResponseModel;

public class CreatePaymentResponseModel : BaseModel
{
    public string Url { get; set; } = null!;
    public string ProvidedId { get; set; } = null!;
}