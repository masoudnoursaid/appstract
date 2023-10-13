using Application;
using AutoMapper.Internal;
using Serilog;
using Web.Common.Extensions;
using Web.Common.Middlewares;

namespace Global.Mvc;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    private IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.SetupControllers()
            .SetupRepositories(Configuration)
            .SetupMediateR()
            .DbInitByEnv(Configuration);

        services.AddControllersWithViews();

        services.AddCors(options =>
        {
            options.AddPolicy("AllowOrigin",
                corsPolicyBuilder =>
                {
                    corsPolicyBuilder.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
        });

        services.AddAutoMapper(cfg => { cfg.Internal().MethodMappingEnabled = false; });

        services.AddApplication();
    }


    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseCors("AllowOrigin");
        app.UseSerilogRequestLogging();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthorization();
        app.UseMiddleware<ErrorLoggingMiddleware>();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                "default",
                "{controller=Home}/{action=Index}/{id?}");
        });
    }
}