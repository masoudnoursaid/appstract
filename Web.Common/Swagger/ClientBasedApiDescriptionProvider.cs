using System.Globalization;
using ErrorHandling.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;

namespace Web.Common.Swagger;

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
        // Method intentionally left empty.
    }

    public void OnProvidersExecuted(ApiDescriptionProviderContext context)
    {
        string format = _options.Value.GroupNameFormat;
        CultureInfo culture = CultureInfo.CurrentCulture;
        IList<ApiDescription> results = context.Results;
        List<ApiDescription> sharedApiDescriptions = new();

        foreach (ApiDescription apiDescription in results)
        {
            ApiVersion apiVersion = apiDescription.GetApiVersion();
            string version = apiVersion.ToString(format, culture);
            HandlerCode? handlerCode = apiDescription.GetHandlerCode(out Type? _);

            if (handlerCode is not null)
            {
                Client clientCode = (Client)(((int)handlerCode.Value % 1000) / 100);

                if (clientCode == Client.CustomerShared)
                {
                    ApiDescription customerWebApiDescription = apiDescription.Clone();
                    customerWebApiDescription.GroupName = $"{Client.CustomerWeb}-{version}";
                    sharedApiDescriptions.Add(customerWebApiDescription);
                    apiDescription.GroupName = $"{Client.CustomerMobile}-{version}";
                }
                else
                {
                    apiDescription.GroupName = $"{clientCode}-{version}";
                }
            }
        }

        sharedApiDescriptions.ForEach(results.Add);
    }
}