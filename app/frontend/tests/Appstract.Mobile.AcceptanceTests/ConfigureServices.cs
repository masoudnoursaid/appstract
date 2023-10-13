using Appstract.Mobile.AcceptanceTests.Drivers;
using Appstract.Mobile.AcceptanceTests.Drivers.Implementation;

namespace Appstract.Mobile.AcceptanceTests;

public static class ConfigureServices
{
    public static IServiceCollection RegisterDrivers(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IAppiumDriver, LocalAppiumDriver>();
        return serviceCollection;
    }
}