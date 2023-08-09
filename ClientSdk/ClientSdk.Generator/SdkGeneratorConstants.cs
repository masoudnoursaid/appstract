namespace ClientSdk.Generator;

public static class SdkGeneratorConstants
{
    public const string MOBILE_PROJECT_NAME = "ClientSdk.Customer.Mobile";
    public const string MOBILE_PROJECT_NAMESPACE = $"{MOBILE_PROJECT_NAME}.V{{Version}}";
    public const string MOBILE_SWAGGER_ENDPOINT = "/_sw/CustomerMobile-v{Version}/swagger.json";
    public const string WEB_PROJECT_NAME = "ClientSdk.Customer.Web";
    public const string WEB_PROJECT_NAMESPACE = $"{WEB_PROJECT_NAME}.V{{Version}}";
    public const string WEB_SWAGGER_ENDPOINT = "/_sw/CustomerWeb-v{Version}/swagger.json";
}