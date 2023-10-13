namespace Infrastructure.Services.BillPlz.Model;

internal sealed class BillPlzVerifyHmacResponseModel
{
    public BillPlzVerifyHmacResponseModel(bool verified)
    {
        Verified = verified;
    }

    public bool Verified { get; set; }
}