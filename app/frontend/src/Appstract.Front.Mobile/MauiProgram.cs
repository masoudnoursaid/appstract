using Appstract.Front.Mobile.Common;
using Appstract.Front.Mobile.Infrastructure.Handler;
using Appstract.Front.Mobile.Infrastructure.Services;
using Appstract.Mobile.Application.Common.Consts;
using Appstract.Mobile.Application.Services;
using Appstract.Mobile.Infrastructure.Services;
using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using Mopups.Hosting;
using Mopups.Services;
using The49.Maui.BottomSheet;
using UltraTone;
using UltraTone.ClientSdk.Customer.Mobile.V1;
using ILogger = Appstract.Mobile.Application.Services.ILogger;

namespace Appstract.Front.Mobile;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        MauiAppBuilder builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseSentry(options =>
            {
                options.Dsn = string.Empty;
                options.Debug = false;
                options.TracesSampleRate = 1.0;
            })
            .UseMauiCommunityToolkit()
            .ConfigureMopups()
            .UseBottomSheet()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

#if DEBUG
        builder.Logging.AddDebug();
#endif
        builder.Services.AddSingleton(DeviceInfo.Current);
        builder.Services.AddSingleton(AppInfo.Current);
        builder.Services.AddSingleton(DeviceDisplay.Current);
        builder.Services.AddSingleton(MopupService.Instance);
        builder.Services.AddSingleton(WebAuthenticator.Default);
        builder.Services.AddSingleton(AppleSignInAuthenticator.Default);
        builder.Services.AddSingleton(SecureStorage.Default);

        builder.Services.AddHttpClient(string.Empty, sp =>
        {
            sp.BaseAddress = new Uri(Route.BASE_URL);
        }).AddHttpMessageHandler<TokenAuthHeaderHandler>();
        builder.Services.AddSingleton<TokenAuthHeaderHandler>();
        builder.Services.AddSingleton<IAuthService, AuthService>();
        builder.Services.AddTransient<IAuthClient, AuthClient>();

        builder.Services.AddTransient<ILogger, LoggerService>();

        builder.Services.AddSingleton<INavigationService, NavigationService>();

        return builder.Build();
    }
}