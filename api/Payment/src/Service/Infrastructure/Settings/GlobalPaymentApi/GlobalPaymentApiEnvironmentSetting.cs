using Application.Common.Uri;
using Application.Settings.Environments;
using Application.Settings.Environments.GlobalPaymentApi;
using Infrastructure.Common.Uri;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Settings.GlobalPaymentApi;

public class GlobalPaymentApiEnvironmentSetting : EnvironmentSetting, IGlobalPaymentApiEnvironmentSetting
{
    [ConfigurationKeyName("GLOBAL_PAYMENT_HOST")]
    public string RedirectPaymentHost { get; set; } = null!;

    [ConfigurationKeyName("GLOBAL_PAYMENT_PORT")]
    public string RedirectPaymentPort { get; set; } = null!;

    [ConfigurationKeyName("GLOBAL_PAYMENT_SCHEMA")]
    public string RedirectPaymentSchema { get; set; } = null!;

    public Uri Uri =>
        new(RedirectPaymentSchema + "://" + RedirectPaymentHost +
            (RedirectPaymentPort == null ? string.Empty : $":{RedirectPaymentPort}"));


    public IPaypalUri PaypalUri => new PaypalUri(Uri.AbsoluteUri);

    public IBillPlzUri BillPlzUri => new BillPlzUri(Uri.AbsoluteUri);
    public IStripeUri StripeUri => new StripeUri(Uri.AbsoluteUri);
    public IMollieUri MollieUri => new MollieUri(Uri.AbsoluteUri);
}