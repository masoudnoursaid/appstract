using Appstract.Web.Extensions;
using Appstract.WebApi.Middlewares;
using Infrastructure;
using Payment.Sdk.Common.Enum;
using Payment.Sdk.DI;
using Payment.Sdk.Middleware;
using Serilog;

namespace Appstract.WebApi;

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
            .SetupMediateR();


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

        services.AddInfrastructure();
        
        services.ConfigurePayHubClient(cnf =>
        {
            // Set payment microservice domain
            cnf.ConnectionConfiguration.Address = "http://localhost:9090";
            cnf.ConnectionConfiguration.Timeout = 100;
            // your webhook address
            cnf.ConnectionConfiguration.WebHook = "http://localhost:7107/hook";

            // Security method between company applications and payment hub
            cnf.SecurityConfiguration.Method = SecurityMethod.HMAC;

            // Your application api secret
            cnf.SecurityConfiguration.ApiSecret = "e8db9d0fe90b10489c1e739ab818311000817316dacb37cbc861be2ed02f7a24";

            // Your application api key
            cnf.SecurityConfiguration.ApiKey = "NL_584216efed319bc206d28231485c5bd152f94d21e4bf7dbcbe151b61f871295c";
        });

    }


    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.SetupSwaggerUI();
        app.UseCors("AllowOrigin");
        app.UseSerilogRequestLogging();
        app.UseStaticFiles();
        app.UseRouting();
        app.UsePayHubCallBackAuthentication();
        app.UseAuthorization();
        app.UseMiddleware<ErrorLoggingMiddleware>();
        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }
}