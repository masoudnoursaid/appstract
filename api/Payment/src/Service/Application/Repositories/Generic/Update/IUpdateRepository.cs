using Domain.Common.BaseTypes;

namespace Application.Repositories.Generic.Update;

public interface IUpdateRepository<TEntity> : IUpdateRepository<TEntity, string>
    where TEntity : BaseEntity<string>
{
}

public interface IUpdateRepository<TEntity, TId>
    where TId : class
    where TEntity : BaseEntity<TId>
{
    Task Update(TEntity entity, bool exceptionRaiseIfNotExist = false);
}