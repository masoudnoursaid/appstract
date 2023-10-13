using Application.Common.Uri;
using Infrastructure.Common.Consts.EndPoints;

namespace Infrastructure.Common.Uri;

public class StripeUri : System.Uri, IStripeUri
{
    public StripeUri(string url) : base(url + GlobalPaymentMvc.STRIPE_CONTROLLER)
    {
    }

    public System.Uri RedirectVerify => new(AbsoluteUri + "/" + GlobalPaymentMvc.REDIRECT_VERIFY);
    public System.Uri CallBackVerify => new(AbsoluteUri + "/" + GlobalPaymentMvc.CALLBACK_VERIFY);
    public System.Uri Cancel => new(AbsoluteUri + "/" + GlobalPaymentMvc.CANCEL);
}

