using System.Linq.Expressions;
using Application.Repositories.Generic.Delete;
using Domain.Common.BaseTypes;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Sql.GenericRepository.Delete;

public class DeleteRepository<TEntity, TContext> : DeleteRepository<TEntity, string, TContext>,
    IDeleteRepository<TEntity>
    where TContext : DbContext
    where TEntity : BaseEntity<string>
{
    public DeleteRepository(TContext context) : base(context)
    {
    }
}

public class DeleteRepository<TEntity, TId, TContext> : BaseDeleteRepository<TContext>,
    IDeleteRepository<TEntity, TId>
    where TId : class
    where TContext : DbContext
    where TEntity : BaseEntity<TId>
{
    public DeleteRepository(TContext context) : base(context)
    {
    }

    public async Task Delete(TEntity entity, bool exceptionRaiseIfNotExist = false)
    {
        await Delete<TEntity, TId>(entity, exceptionRaiseIfNotExist);
    }

    public async Task Delete(TId id, bool exceptionRaiseIfNotExist = false)
    {
        await Delete<TEntity, TId>(id, exceptionRaiseIfNotExist);
    }

    public async Task Delete(Expression<Func<TEntity, bool>> single, bool exceptionRaiseIfNotExist = false)
    {
        await Delete<TEntity, TId>(single, exceptionRaiseIfNotExist);
    }

    public async Task DeleteRange(IEnumerable<TEntity> entities)
    {
        await DeleteRange<TEntity, TId>(entities);
    }

    public async Task DeleteRange(Expression<Func<TEntity, bool>> where)
    {
        await DeleteRange<TEntity, TId>(where);
    }

    public async Task SoftDelete(TId id, bool exceptionRaiseIfNotExist = false)
    {
        await SoftDelete<TEntity, TId>(id, exceptionRaiseIfNotExist);
    }
}