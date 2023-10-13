namespace Infrastructure.Common.Consts.EndPoints;

public abstract class GlobalPaymentMvc
{
    public const string PAYPAL_CONTROLLER = "paypal";
    public const string BILLPLZ_CONTROLLER = "billplz";
    public const string STRIPE_CONTROLLER = "stripe";
    public const string MOLLIE_CONTROLLER = "mollie";
    public const string VERIFY = "verify";
    public const string REDIRECT_VERIFY = VERIFY;
    public const string CALLBACK_VERIFY = "webhook_verify";
    public const string CANCEL = "cancel";
}