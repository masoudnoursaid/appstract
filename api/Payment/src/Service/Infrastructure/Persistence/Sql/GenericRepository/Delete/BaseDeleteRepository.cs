﻿using System.Linq.Expressions;
using Domain.Common.BaseTypes;
using Domain.Exceptions.Entity;
using Microsoft.EntityFrameworkCore;

#pragma warning disable CS8604

namespace Infrastructure.Persistence.Sql.GenericRepository.Delete;

public class BaseDeleteRepository<TContext> : BaseRepository<TContext>
    where TContext : DbContext
{
    public BaseDeleteRepository(TContext context) : base(context)
    {
    }

    public async Task Delete<TEntity, TId>(TEntity entity, bool exceptionRaiseIfNotExist = false)
        where TId : class
        where TEntity : BaseEntity<TId>
    {
        var data = await base.Table<TEntity, TId>().FindAsync(entity);

        if (exceptionRaiseIfNotExist && data == null)
            throw new CanNotFoundEntityException(entity.Id);

        base.Table<TEntity, TId>().Remove(entity);
        await base.SaveChanges();
    }

    public async Task Delete<TEntity, TId>(TId id, bool exceptionRaiseIfNotExist = false)
        where TId : class
        where TEntity : BaseEntity<TId>
    {
        var data = await base.Table<TEntity, TId>().FindAsync(id);

        if (exceptionRaiseIfNotExist && data == null)
            throw new CanNotFoundEntityException(id);

        base.Table<TEntity, TId>().Remove(data!);
        await base.SaveChanges();
    }

    public async Task DeleteRange<TEntity, TId>(IEnumerable<TEntity> entities)
        where TId : class
        where TEntity : BaseEntity<TId>
    {
        base.Table<TEntity, TId>().RemoveRange(entities);
        await base.SaveChanges();
    }


    public async Task Delete<TEntity, TId>(Expression<Func<TEntity, bool>> single,
        bool exceptionRaiseIfNotExist = false)
        where TId : class
        where TEntity : BaseEntity<TId>
    {
        var data = base.Table<TEntity, TId>().SingleOrDefault(single);

        if (exceptionRaiseIfNotExist && data == null)
            throw new CanNotFoundEntityException(single.ToString());

        base.Table<TEntity, TId>().Remove(data);
        await base.SaveChanges();
    }


    public async Task DeleteRange<TEntity, TId>(Expression<Func<TEntity, bool>> where)
        where TId : class
        where TEntity : BaseEntity<TId>
    {
        var data = base.Table<TEntity, TId>().Where(where);

        base.Table<TEntity, TId>().RemoveRange(data);

        await base.SaveChanges();
    }


    public async Task SoftDelete<TEntity, TId>(TId id, bool exceptionRaiseIfNotExist = false)
        where TId : class
        where TEntity : BaseEntity<TId>
    {
        var entity = await base.Table<TEntity, TId>().FindAsync(id);

        if (exceptionRaiseIfNotExist && entity == null)
            throw new CanNotFoundEntityException(id);

        if (entity != null)
        {
            entity.SoftDelete();
            entity.ModifiedDateTime = DateTime.UtcNow;

            await base.SaveChanges();
        }
    }
}