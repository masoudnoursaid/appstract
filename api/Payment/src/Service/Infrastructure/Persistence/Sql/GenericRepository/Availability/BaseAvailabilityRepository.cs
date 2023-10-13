using System.Linq.Expressions;
using Domain.Common.BaseTypes;
using Domain.Exceptions.Entity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Sql.GenericRepository.Availability;

public class BaseAvailabilityRepository<TContext> : BaseRepository<TContext>
    where TContext : DbContext
{
    public BaseAvailabilityRepository(TContext context) : base(context)
    {
    }


    public async Task<bool> Any<TEntity, TId>(Expression<Func<TEntity, bool>> any, string navigationPropertyPath)
        where TId : class
        where TEntity : BaseEntity<TId>
    {
        return await Any<TEntity, TId>(base.IncludeByPath<TEntity, TId>(navigationPropertyPath), any);
    }


    public async Task<bool> Any<TEntity, TId>(Expression<Func<TEntity, bool>> any)
        where TId : class
        where TEntity : BaseEntity<TId>
    {
        return await Any<TEntity, TId>(base.Query<TEntity, TId>(), any);
    }


    public async Task<bool> Any<TEntity, TId>(IQueryable<TEntity> entities, Expression<Func<TEntity, bool>> any)
        where TId : class
        where TEntity : BaseEntity<TId>
    {
        var result = await entities.AnyAsync(any);
        return result;
    }


    public async Task CheckAvailability<TEntity, TId>(Expression<Func<TEntity, bool>> any,
        string navigationPropertyPath)
        where TId : class
        where TEntity : BaseEntity<TId>
    {
        await CheckAvailability<TEntity, TId>(base.IncludeByPath<TEntity, TId>(navigationPropertyPath), any);
    }


    public async Task CheckAvailability<TEntity, TId>(Expression<Func<TEntity, bool>> any)
        where TId : class
        where TEntity : BaseEntity<TId>
    {
        await CheckAvailability<TEntity, TId>(base.Query<TEntity, TId>(), any);
    }


    public async Task CheckAvailability<TEntity, TId>(IQueryable<TEntity> entities,
        Expression<Func<TEntity, bool>> any)
        where TId : class
        where TEntity : BaseEntity<TId>
    {
        var result = await entities.AnyAsync(any);

        if (!result)
            throw new CanNotFoundEntityException(any.ToString());
    }
}