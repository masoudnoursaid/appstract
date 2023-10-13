namespace Application.Common.Consts.Authentication;

public abstract class HmacAuthentication
{
    public const string SIGNATURE_HEADER = "hmac-sign";
    public const string DATE_HEADER = "x-ms-date";
    public const string NONCE_HEADER = "x-ms-nonce";
    public const string API_KEY_HEADER = "api-key";
}