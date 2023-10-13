using System.Linq.Expressions;
using Application.Repositories.Generic.Availability;
using Domain.Common.BaseTypes;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Sql.GenericRepository.Availability;

public class AvailabilityRepository<TEntity, TContext> : AvailabilityRepository<TEntity, string, TContext>,
    IAvailabilityRepository<TEntity>
    where TContext : DbContext
    where TEntity : BaseEntity<string>
{
    public AvailabilityRepository(TContext context) : base(context)
    {
    }
}

public class AvailabilityRepository<TEntity, TId, TContext> : BaseAvailabilityRepository<TContext>,
    IAvailabilityRepository<TEntity, TId>
    where TId : class
    where TContext : DbContext
    where TEntity : BaseEntity<TId>
{
    public AvailabilityRepository(TContext context) : base(context)
    {
    }


    public async Task<bool> Any(Expression<Func<TEntity, bool>> any, string navigationPropertyPath)
    {
        return await Any<TEntity, TId>(any, navigationPropertyPath);
    }

    public async Task<bool> Any(Expression<Func<TEntity, bool>> any)
    {
        return await Any<TEntity, TId>(any);
    }

    public async Task<bool> Any(IQueryable<TEntity> entities, Expression<Func<TEntity, bool>> any)
    {
        return await Any<TEntity, TId>(entities, any);
    }


    public async Task CheckAvailability(Expression<Func<TEntity, bool>> any, string navigationPropertyPath)
    {
        await CheckAvailability<TEntity, TId>(any, navigationPropertyPath);
    }

    public async Task CheckAvailability(Expression<Func<TEntity, bool>> any)
    {
        await CheckAvailability<TEntity, TId>(any);
    }

    public async Task CheckAvailability(IQueryable<TEntity> entities, Expression<Func<TEntity, bool>> any)
    {
        await CheckAvailability<TEntity, TId>(entities, any);
    }
}