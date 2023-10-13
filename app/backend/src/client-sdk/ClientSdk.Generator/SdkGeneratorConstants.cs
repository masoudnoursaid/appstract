namespace ClientSdk.Generator;

public static class SdkGeneratorConstants
{
    public const string WEB_PROJECT_NAME = "ClientSdk.WebApi";
    public const string WEB_PROJECT_NAMESPACE = $"{WEB_PROJECT_NAME}.V{{Version}}";
    public const string WEB_SWAGGER_ENDPOINT = "/_sw/v{Version}/swagger.json";
}