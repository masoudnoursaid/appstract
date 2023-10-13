using Infrastructure.Persistence.Sql;
using Infrastructure.Persistence.Sql.Seeds;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Test.Common.Setting;

namespace Test.Common.Extensions;

public static class TestStartUpExtensions
{
    /// <summary>
    ///     Startup of test project to run services form .env files <see cref="TestSetting" />
    /// </summary>
    public static async Task TestRUp(this IServiceProvider provider)
    {
        await provider.DbCleanUp();
        await provider.SeedUp();
    }


    public static async Task SeedUp(this IServiceProvider provider)
    {
        var configuration = provider.GetRequiredService<IConfiguration>();
        var setting = configuration.Get<TestSetting>()!;
        if (setting.SeedUp)
        {
            using var scope = provider.CreateScope();
            var seedService = scope.ServiceProvider.GetRequiredService<ISeedService>();
            await seedService.StartAll();
        }
    }

    public static async Task DbCleanUp(this IServiceProvider provider)
    {
        var configuration = provider.GetRequiredService<IConfiguration>();
        var setting = configuration.Get<TestSetting>()!;
        if (setting.DbCleanUp)
        {
            using var scope = provider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<PaymentDbContext>();
            await dbContext.Database.EnsureDeletedAsync();
            await dbContext.Database.MigrateAsync();
        }
    }

    public static TService GetRequiredServiceFromScope<TService>(this IServiceProvider provider) where TService : class
    {
        using var scope = provider.CreateScope();
        var service = scope.ServiceProvider.GetRequiredService<TService>();
        return service;
    }
}