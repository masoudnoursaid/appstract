using Domain.Common.BaseTypes;
using Domain.Exceptions.Entity;
using Microsoft.EntityFrameworkCore;

//Resharper disable all

namespace Infrastructure.Persistence.Sql.GenericRepository.Update;

public class BaseUpdateRepository<TContext> : BaseRepository<TContext>
    where TContext : DbContext
{
    public BaseUpdateRepository(TContext context) : base(context)
    {
    }


    public async Task Update<TEntity, TId>(TEntity entity, bool exceptionRaiseIfNotExist = false)
        where TId : class
        where TEntity : BaseEntity<TId>
    {
        var data = await base.Table<TEntity, TId>().FindAsync(entity.Id);

        if (exceptionRaiseIfNotExist && data == null)
            throw new CanNotFoundEntityException(entity.Id);


        if (entity != null)
        {
            base.ChangeState<TEntity, TId>(data!, EntityState.Detached);
            base.Table<TEntity, TId>().Update(entity);
            await base.SaveChanges();
        }
    }
}