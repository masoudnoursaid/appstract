using Application.Common.Uri;
using Infrastructure.Common.Consts.EndPoints;

namespace Infrastructure.Common.Uri;

public class PaypalUri : System.Uri, IPaypalUri
{
    public PaypalUri(string url) : base(url + GlobalPaymentMvc.PAYPAL_CONTROLLER)
    {
    }

    public System.Uri Verify => new(AbsoluteUri + "/" + GlobalPaymentMvc.VERIFY);
    public System.Uri Cancel => new(AbsoluteUri + "/" + GlobalPaymentMvc.CANCEL);
}