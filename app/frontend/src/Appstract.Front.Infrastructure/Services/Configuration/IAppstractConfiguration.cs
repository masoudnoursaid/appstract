namespace Appstract.Front.Infrastructure.Services.Configuration;

public interface IAppstractConfiguration
{
    bool UseFakeBackend { get; }
    string? BackendUrl { get; }
    Uri BaseAddress { get; }
}