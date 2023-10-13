using Application.Common.Uri;

namespace Application.Settings.Environments.GlobalPaymentApi;

public interface IGlobalPaymentApiEnvironmentSetting
{
    string RedirectPaymentHost { get; set; }
    string RedirectPaymentPort { get; set; }
    string RedirectPaymentSchema { get; set; }

    Uri Uri { get; }
    IPaypalUri PaypalUri { get; }
    IBillPlzUri BillPlzUri { get; }
    IStripeUri StripeUri { get; }
    IMollieUri MollieUri { get; }
}