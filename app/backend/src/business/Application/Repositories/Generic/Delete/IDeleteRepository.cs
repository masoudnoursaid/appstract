using System.Linq.Expressions;
using Domain.Common.BaseTypes;

namespace Application.Repositories.Generic.Delete;

public interface IDeleteRepository<TEntity> : IDeleteRepository<TEntity, string>
    where TEntity : BaseEntity<string>
{
}

public interface IDeleteRepository<TEntity, TId>
    where TId : class
    where TEntity : BaseEntity<TId>
{
    Task Delete(TEntity entity, bool exceptionRaiseIfNotExist = false);
    Task Delete(TId id, bool exceptionRaiseIfNotExist = false);
    Task Delete(Expression<Func<TEntity, bool>> single, bool exceptionRaiseIfNotExist = false);

    Task DeleteRange(IEnumerable<TEntity> entities);
    Task DeleteRange(Expression<Func<TEntity, bool>> where);

    Task SoftDelete(TId id, bool exceptionRaiseIfNotExist = false);
}