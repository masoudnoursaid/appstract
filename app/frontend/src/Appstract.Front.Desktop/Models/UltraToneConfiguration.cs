using Appstract.Front.Infrastructure.Services.Configuration;

namespace Appstract.Desktop.Models;

public class UltraToneConfiguration : IUltraToneConfiguration
{
    public bool UseFakeBackend => false;
    public string BackendUrl => string.Empty;
    public Uri BaseAddress => new Uri("https://endpoints-dev.ultratone.dev/");
}

