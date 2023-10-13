using Application.Common.Uri;
using Infrastructure.Common.Consts.EndPoints;

namespace Infrastructure.Common.Uri;

public class BillPlzUri : System.Uri, IBillPlzUri
{
    public BillPlzUri(string url) : base(url + GlobalPaymentMvc.BILLPLZ_CONTROLLER)
    {
    }

    public System.Uri RedirectVerify => new(AbsoluteUri + "/" + GlobalPaymentMvc.REDIRECT_VERIFY);
    public System.Uri CallBackVerify => new(AbsoluteUri + "/" + GlobalPaymentMvc.CALLBACK_VERIFY);
    public System.Uri Cancel => new(AbsoluteUri + "/" + GlobalPaymentMvc.CANCEL);
}