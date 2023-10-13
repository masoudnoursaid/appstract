using Infrastructure.Persistence.Sql;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DbConnectionInterceptor = Infrastructure.Persistence.Sql.Interceptors.DbConnectionInterceptor;
using SaveChangesInterceptor = Infrastructure.Persistence.Sql.Interceptors.SaveChangesInterceptor;

namespace Infrastructure.DependencyInjection;

public static class DependencyInjection
{
    private const string MIGRATIONS_ASSEMBLY = "Migrations";

    public static IServiceCollection AddDbContext(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var settings = configuration.Get<DbSettings>()!;
        var str = GetAppDbConnectionString(settings);

        services.AddDbContext<PaymentDbContext>(opt =>
        {
            opt.UseMySQL(str, s
                => s.MigrationsAssembly(MIGRATIONS_ASSEMBLY)).AddInterceptors(GetInterceptors());
            opt.EnableSensitiveDataLogging();
        });

        return services;
    }


    private static string GetAppDbConnectionString(DbSettings settings)
    {
        var result =
            $"server={settings.DbAppHost};Port={settings.DbAppPort};database={settings.DbAppName};uid={settings.DbAppUser};Pwd={settings.DbAppPass};";
        return result;
    }


    private static IInterceptor[] GetInterceptors()
    {
        return new IInterceptor[]
        {
            new DbConnectionInterceptor(),
            new SaveChangesInterceptor()
        };
    }
}