using System.Linq.Expressions;
using Domain.Common.BaseTypes;

namespace Application.Repositories.Generic.Create;

public interface ICreateRepository<TEntity> : ICreateRepository<TEntity, string>
    where TEntity : BaseEntity<string>
{
}

public interface ICreateRepository<TEntity, TId>
    where TId : class
    where TEntity : BaseEntity<TId>
{
    Task Insert(TEntity entity);
    Task InsertRange(IEnumerable<TEntity> entities);
    Task ClearAllEntitiesThenAddRange(IEnumerable<TEntity> insertEntities);
    Task ClearRemoveListThenAddRange(IEnumerable<TEntity> removeList, IEnumerable<TEntity> insertEntities);
    Task ReCreate(Expression<Func<TEntity, bool>> deleteCondition, IEnumerable<TEntity> insertEntities);
}