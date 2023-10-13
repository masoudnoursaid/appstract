namespace ClientSdk.Generator;

public static class SdkGeneratorConstants
{
    public const string ADMIN_API_SDK_PROJECT_NAME = "ClientSdk.AdminApi";
    public const string ADMIN_API_SDK_PROJECT_NAMESPACE = $"{ADMIN_API_SDK_PROJECT_NAME}.V{{Version}}";
    public const string ADMIN_API_SWAGGER_ENDPOINT = "/_sw/v{Version}/swagger.json";


    public const string PAYMENT_API_SDK_PROJECT_NAME = "ClientSdk.PaymentApi";
    public const string PAYMENT_API_SDK_PROJECT_NAMESPACE = $"{PAYMENT_API_SDK_PROJECT_NAME}.V{{Version}}";
    public const string PAYMENT_API_SWAGGER_ENDPOINT = "/_sw/v{Version}/swagger.json";


    public const string ADMIN_API_KEY = nameof(ADMIN_API_KEY);
    public const string PAYMENT_API_KEY = nameof(PAYMENT_API_KEY);
}