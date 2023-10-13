using Application.Common.BaseTypes.Model;

namespace Application.Common.ResponseModel;

public abstract class VerifyPaymentResponseModel : BaseModel
{
    protected VerifyPaymentResponseModel(string payerProvidedId, bool verified)
    {
        PayerProvidedId = payerProvidedId;
        Verified = verified;
    }

    public string PayerProvidedId { get; set; }
    public bool Verified { get; set; }
    public string? Email { get; set; }
    public string? CountryCode { get; set; }
    public string? City { get; set; }
    public string? Address { get; set; }
    public string? Firstname { get; set; }
    public string? Lastname { get; set; }
    public string? PostalCode { get; set; }
}