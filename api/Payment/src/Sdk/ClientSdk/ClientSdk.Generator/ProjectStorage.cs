namespace ClientSdk.Generator;

public static class ProjectStorage
{
    private static readonly List<ProjectModel> Projects = new()
    {
        AdminApi(),
        PaymentApi()
    };

    public static ProjectModel? Get(string projectName)
    {
        return Projects.FirstOrDefault(p => p.Name == projectName);
    }

    private static ProjectModel AdminApi()
    {
        return new ProjectModel(SdkGeneratorConstants.ADMIN_API_SDK_PROJECT_NAME,
            SdkGeneratorConstants.ADMIN_API_SDK_PROJECT_NAMESPACE
            , SdkGeneratorConstants.ADMIN_API_SWAGGER_ENDPOINT);
    }

    private static ProjectModel PaymentApi()
    {
        return new ProjectModel(SdkGeneratorConstants.PAYMENT_API_SDK_PROJECT_NAME,
            SdkGeneratorConstants.PAYMENT_API_SDK_PROJECT_NAMESPACE
            , SdkGeneratorConstants.PAYMENT_API_SWAGGER_ENDPOINT);
    }
}