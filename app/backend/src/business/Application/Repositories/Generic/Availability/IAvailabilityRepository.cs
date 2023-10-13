using System.Linq.Expressions;
using Domain.Common.BaseTypes;

namespace Application.Repositories.Generic.Availability;

public interface IAvailabilityRepository<TEntity> : IAvailabilityRepository<TEntity, string>
    where TEntity : BaseEntity<string>
{
}

public interface IAvailabilityRepository<TEntity, TId>
    where TId : class
    where TEntity : BaseEntity<TId>
{
    Task<bool> Any(Expression<Func<TEntity, bool>> any, string navigationPropertyPath);

    Task<bool> Any(Expression<Func<TEntity, bool>> any);

    Task<bool> Any(IQueryable<TEntity> entities, Expression<Func<TEntity, bool>> any);


    Task CheckAvailability(Expression<Func<TEntity, bool>> any, string navigationPropertyPath);

    Task CheckAvailability(Expression<Func<TEntity, bool>> any);

    Task CheckAvailability(IQueryable<TEntity> entities, Expression<Func<TEntity, bool>> any);
}