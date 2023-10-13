using System.Linq.Expressions;
using Application.Repositories.Generic.Create;
using Domain.Common.BaseTypes;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Sql.GenericRepository.Create;

public class CreateRepository<TEntity, TContext> : CreateRepository<TEntity, string, TContext>,
    ICreateRepository<TEntity>
    where TContext : DbContext
    where TEntity : BaseEntity<string>
{
    public CreateRepository(TContext context) : base(context)
    {
    }
}

public class CreateRepository<TEntity, TId, TContext> : BaseCreateRepository<TContext>, ICreateRepository<TEntity, TId>
    where TId : class
    where TContext : DbContext
    where TEntity : BaseEntity<TId>
{
    public CreateRepository(TContext context) : base(context)
    {
    }

    public async Task ClearAllEntitiesThenAddRange(IEnumerable<TEntity> insertEntities)
    {
        await ClearAllEntitiesThenAddRange<TEntity, TId>(insertEntities);
    }

    public async Task ClearRemoveListThenAddRange(IEnumerable<TEntity> removeList, IEnumerable<TEntity> insertEntities)
    {
        await ClearRemoveListThenAddRange<TEntity, TId>(removeList, insertEntities);
    }

    public async Task Insert(TEntity entity)
    {
        await Insert<TEntity, TId>(entity);
    }

    public async Task InsertRange(IEnumerable<TEntity> entities)
    {
        await InsertRange<TEntity, TId>(entities);
    }

    public async Task ReCreate(Expression<Func<TEntity, bool>> deleteCondition, IEnumerable<TEntity> insertEntities)
    {
        await ReCreate<TEntity, TId>(deleteCondition, insertEntities);
    }
}