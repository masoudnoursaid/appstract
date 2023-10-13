using Infrastructure.Common;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sentry;
using Sentry.Extensibility;
using Serilog;
using Serilog.Events;

namespace Appstract.Web.Extensions;

public static class SentryExtenstion
{
    public static IHostBuilder SetupSerilogWithSentry(this IHostBuilder builder)
    {
        var result = builder.UseSerilog((context, serviceProvider, configuration) =>
        {
            configuration
                .ReadFrom.Configuration(context.Configuration)
                .ReadFrom.Services(serviceProvider)
                .WriteTo.Sentry(options =>
                {
                    options.Dsn = context.Configuration.Get<SentrySetting>()?.Dsn;
                    options.MinimumEventLevel = LogEventLevel.Warning;
                    options.MinimumBreadcrumbLevel = LogEventLevel.Debug;
                    options.ReportAssembliesMode = ReportAssembliesMode.None;
                    options.Debug = false;
                    options.AddEventProcessorProvider(() => new List<ISentryEventProcessor>
                    {
                        serviceProvider.GetRequiredService<CustomSentryEventProcessor>()
                    });
                });
        });
        return result;
    }
}