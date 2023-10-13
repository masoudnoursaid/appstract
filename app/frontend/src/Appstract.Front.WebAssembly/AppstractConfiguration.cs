using System.Diagnostics.CodeAnalysis;
using Appstract.Front.Infrastructure.Services.Configuration;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace Appstract.Front.Client;

[ExcludeFromCodeCoverage]
public class AppstractConfiguration : IAppstractConfiguration
{
    private readonly WebAssemblyHostBuilder _builder;

    public AppstractConfiguration(WebAssemblyHostBuilder builder)
    {
        _builder = builder;
        if (UseFakeBackend)
        {
            BaseAddress = new Uri(builder.HostEnvironment.BaseAddress);
        }
        else
        {
            string? apiUrl = BackendUrl;
            if (string.IsNullOrEmpty(apiUrl))
            {
                apiUrl = builder.HostEnvironment.BaseAddress.Replace("web", "endpoints");
            }
            BaseAddress = new Uri(apiUrl);
        }
    }

    public bool UseFakeBackend => _builder.Configuration.GetValue<bool>("UseFakeBackend");
    public string? BackendUrl => _builder.Configuration.GetValue<string>("BackendUrl");
    public Uri BaseAddress { get; }
}

