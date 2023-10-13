using Application.Repositories.Generic.Update;
using Domain.Common.BaseTypes;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Sql.GenericRepository.Update;

public class UpdateRepository<TEntity, TContext> : UpdateRepository<TEntity, string, TContext>,
    IUpdateRepository<TEntity>
    where TContext : DbContext
    where TEntity : BaseEntity<string>
{
    public UpdateRepository(TContext context) : base(context)
    {
    }
}

public class UpdateRepository<TEntity, TId, TContext> : BaseUpdateRepository<TContext>,
    IUpdateRepository<TEntity, TId>
    where TId : class
    where TContext : DbContext
    where TEntity : BaseEntity<TId>
{
    public UpdateRepository(TContext context) : base(context)
    {
    }

    public async Task Update(TEntity entity, bool exceptionRaiseIfNotExist = false)
    {
        await Update<TEntity, TId>(entity, exceptionRaiseIfNotExist);
    }
}