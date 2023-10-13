using Appstract.Desktop.Models;
using Microsoft.Extensions.Logging;

namespace Appstract.Desktop
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            MauiAppBuilder builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddMauiBlazorWebView();

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif

            builder.Services.AddSingleton<IDeviceInfo>(DeviceInfo.Current);
            builder.Services.AddSingleton<IAppInfo>(AppInfo.Current);
            builder.Services.AddSingleton<IDeviceDisplay>(DeviceDisplay.Current);

            UltraToneConfiguration configuration = new();
            builder.Services.AddUltraToneServices(configuration);

            NativeDeviceDto nativeDeviceDto = new NativeDeviceDto()
            {
                AppVersion = AppInfo.VersionString,
                BuildNumber = AppInfo.BuildString,
                Os = DeviceInfo.Platform.ToString(),
                OsVersion = DeviceInfo.VersionString,
                Manufacturer = DeviceInfo.Manufacturer,
                Model = DeviceInfo.Model,
                IsTablet = DeviceInfo.Idiom == DeviceIdiom.Tablet,
                IsEmulator = false,
                DisplayHeight = Convert.ToInt32(DeviceDisplay.MainDisplayInfo.Height),
                DisplayWidth = Convert.ToInt32(DeviceDisplay.MainDisplayInfo.Width),
                DisplayOrientation = DisplayOrientationType.Landscape,
                DeviceType = UltraTone.ClientSdk.Customer.Web.V1.DeviceType.Desktop
            };

            builder.Services.AddSingleton(nativeDeviceDto);

            return builder.Build();
        }
    }
}