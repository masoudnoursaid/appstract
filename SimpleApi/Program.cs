using System.Reflection;
using Application;
using Application.Common.Services;
using Application.Common.Validators;
using Application.PipelineBehaviors;
using FluentValidation;
using Infrastructure.Services;
using Infrastructure.Validators;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SimpleApi.Middlewares;
using Web.Common.Extensions;
using Web.Common.Swagger;

namespace SimpleApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddSingleton<IIpService, IpService>();
            builder.Services.AddScoped(typeof(IRequestValidator<>), typeof(RequestValidator<>));


            builder.Services.AddApiVersioning(options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
                options.ReportApiVersions = true;
                options.ApiVersionReader = new HeaderApiVersionReader("x-api-version");
            });
            builder.Services.TryAddEnumerable(ServiceDescriptor
                .Transient<IApiDescriptionProvider, ClientBasedApiDescriptionProvider>());

            builder.Services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

            builder.Services.AddCustomSwagger();


            builder.Services.AddMediatR(cfg =>
            {
                cfg.AddOpenBehavior(typeof(CommonErrorsPipelineBehavior<,>));
                cfg.RegisterServicesFromAssemblies(typeof(DependencyInjection).Assembly);
            });
            builder.Services.AddValidatorsFromAssemblies(new List<Assembly> { typeof(DependencyInjection).Assembly });
            var app = builder.Build();

            IApiDescriptionGroupCollectionProvider apiDescriptionGroupCollectionProvider =
                app.Services.GetRequiredService<IApiDescriptionGroupCollectionProvider>();

            if (app.Environment.IsDevelopment())
            {
                app.UseCustomSwaggerUI(apiDescriptionGroupCollectionProvider);
            }

            app.UseMiddleware<ErrorLoggingMiddleware>();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}