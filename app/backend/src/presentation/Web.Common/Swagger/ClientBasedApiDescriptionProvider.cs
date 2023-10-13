using System.Globalization;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;

//Resharper disable all

namespace Appstract.Web.Swagger;

public class ClientBasedApiDescriptionProvider : IApiDescriptionProvider
{
    private readonly IOptions<ApiExplorerOptions> _options;

    public ClientBasedApiDescriptionProvider(IOptions<ApiExplorerOptions> options)
    {
        _options = options;
    }

    public int Order => -1;

    public void OnProvidersExecuting(ApiDescriptionProviderContext context)
    {
    }

    public void OnProvidersExecuted(ApiDescriptionProviderContext context)
    {
        var format = _options.Value.GroupNameFormat;
        var culture = CultureInfo.CurrentCulture;
        var results = context.Results;
        List<ApiDescription> sharedApiDescriptions = new();

        foreach (var apiDescription in results)
        {
            var apiVersion = apiDescription.GetApiVersion();
            var version = apiVersion.ToString(format, culture);
            apiDescription.GroupName = version;
        }

        sharedApiDescriptions.ForEach(results.Add);
    }
}