using Application;
using AutoMapper.Internal;
using Serilog;
using Web.Common.Extensions;
using Web.Common.Middlewares;
using Web.Common.Swagger.Extensions;
using Web.Common.Swagger.Filters;

namespace Payment.Api;

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
            .SetupCustomSwagger()
            .SetupRepositories(Configuration)
            .SetupMediateR()
            .DbInitByEnv(Configuration);

        services.ConfigureSwaggerHmacAuthentication("Swagger.js");
        services.ConfigureSwaggerGen(opt => { opt.OperationFilter<AddApiKeyHeaderParameter>(); });

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
        app.SetupSwaggerUI();
        app.UseCors("AllowOrigin");
        app.UseSerilogRequestLogging();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthorization();
        app.UseMiddleware<ErrorLoggingMiddleware>();
        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }
}