using System.Reflection;
using Application.Repositories.Generic;
using Application.Repositories.Generic.Availability;
using Application.Repositories.Generic.Create;
using Application.Repositories.Generic.Delete;
using Application.Repositories.Generic.Read;
using Application.Repositories.Generic.Update;
using Domain.Common.BaseTypes;
using Infrastructure.Persistence.Sql.GenericRepository;
using Infrastructure.Persistence.Sql.GenericRepository.Availability;
using Infrastructure.Persistence.Sql.GenericRepository.Create;
using Infrastructure.Persistence.Sql.GenericRepository.Delete;
using Infrastructure.Persistence.Sql.GenericRepository.Read;
using Infrastructure.Persistence.Sql.GenericRepository.Update;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Persistence.Sql.Registration;

public static class RepositoryRegistration
{
    public static IServiceCollection RegisterRepositories<TId, TEntity, TContext>(
        this IServiceCollection services)
        where TId : class
        where TEntity : BaseEntity<TId>
        where TContext : DbContext
    {
        services.AddScoped<IEntityRepository<TEntity, TId>, EntityRepository<TEntity, TId>>();
        services.AddScoped<IDeleteRepository<TEntity, TId>, DeleteRepository<TEntity, TId, TContext>>();
        services.AddScoped<IUpdateRepository<TEntity, TId>, UpdateRepository<TEntity, TId, TContext>>();
        services.AddScoped<IReadRepository<TEntity, TId>, ReadRepository<TEntity, TId, TContext>>();
        services.AddScoped<ICreateRepository<TEntity, TId>, CreateRepository<TEntity, TId, TContext>>();
        services
            .AddScoped<IAvailabilityRepository<TEntity, TId>, AvailabilityRepository<TEntity, TId, TContext>>();

        return services;
    }

    public static IServiceCollection RegisterRepositoriesByStrId<TEntity, TContext>(
        this IServiceCollection services)
        where TEntity : BaseEntity<string>
        where TContext : DbContext
    {
        services.AddScoped<IEntityRepository<TEntity>, EntityRepository<TEntity>>();
        services.AddScoped<IDeleteRepository<TEntity>, DeleteRepository<TEntity, TContext>>();
        services.AddScoped<IUpdateRepository<TEntity>, UpdateRepository<TEntity, TContext>>();
        services.AddScoped<IReadRepository<TEntity>, ReadRepository<TEntity, TContext>>();
        services.AddScoped<ICreateRepository<TEntity>, CreateRepository<TEntity, TContext>>();
        services.AddScoped<IAvailabilityRepository<TEntity>, AvailabilityRepository<TEntity, TContext>>();

        return services;
    }

    public static IServiceCollection RegisterRepositoriesByReflection(this IServiceCollection services, Type id,
        Type entity, Type context)
    {
        {
            var methodInfo = typeof(RepositoryRegistration).GetMethods()
                .First(m => m.Name == nameof(RegisterRepositories));
            var method = methodInfo.MakeGenericMethod(id, entity, context);
            _ = method.Invoke(null, new object[] { services });
        }
        {
            var methodInfo = typeof(RepositoryRegistration).GetMethods()
                .First(m => m.Name == nameof(RegisterRepositoriesByStrId));
            var method = methodInfo.MakeGenericMethod(entity, context);
            _ = method.Invoke(null, new object[] { services });
        }

        return services;
    }


    public static IServiceCollection RegisterRepository<TId, TRKContext>(this IServiceCollection services,
        string dllFullname)
        where TId : class
        where TRKContext : DbContext
    {
        var assemblyFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        var target = Assembly.LoadFrom(Path.Combine(assemblyFolder!, dllFullname + ".dll"));

        var entityTypes = target.GetTypes()
            .Where(TheType => TheType.IsClass
                              && !TheType.IsAbstract
                              && TheType.IsSubclassOf(typeof(BaseEntity))
                              && TheType.BaseType == typeof(BaseEntity)
            ).ToList();
        foreach (var entity in entityTypes)
            if (typeof(TId) == typeof(string))
                services.RegisterRepositoriesByReflection(typeof(TId), entity, typeof(TRKContext));
            else
                services.RegisterRepositoriesByReflection(typeof(TId), entity, typeof(TRKContext));


        return services;
    }
}