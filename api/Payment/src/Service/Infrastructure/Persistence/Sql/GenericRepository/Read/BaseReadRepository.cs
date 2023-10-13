using System.Linq.Expressions;
using Application.Common.Consts;
using Domain.Common.BaseTypes;
using Domain.Common.Extension;
using Domain.Common.Util;
using Domain.Exceptions.Entity;
using Microsoft.EntityFrameworkCore;
using MoreLinq;

#pragma warning disable CS8625
#pragma warning disable CS8603
#pragma warning disable CS8604

namespace Infrastructure.Persistence.Sql.GenericRepository.Read;

public class BaseReadRepository<TContext> : BaseRepository<TContext>
    where TContext : DbContext
{
    public BaseReadRepository(TContext context) : base(context)
    {
    }


    public async Task<TEntity> First<TEntity, TId>(string navigationPropertyPath
        , bool track = true
        , bool exceptionRaiseIfNotExist = false)
        where TId : class
        where TEntity : BaseEntity<TId>
    {
        return await First<TEntity, TId>(base.IncludeByPath<TEntity, TId>(navigationPropertyPath), track,
            exceptionRaiseIfNotExist);
    }


    public async Task<TEntity> First<TEntity, TId>(bool includeAllPath = false
        , bool track = true
        , bool exceptionRaiseIfNotExist = false)
        where TId : class
        where TEntity : BaseEntity<TId>
    {
        return includeAllPath
            ? await First<TEntity, TId>(base.IncludeAll<TEntity, TId>(), track, exceptionRaiseIfNotExist)
            : await First<TEntity, TId>(base.Query<TEntity, TId>(), track, exceptionRaiseIfNotExist);
    }


    public async Task<TEntity> First<TEntity, TId>(IQueryable<TEntity> entities
        , bool track = true
        , bool exceptionRaiseIfNotExist = false)
        where TId : class
        where TEntity : BaseEntity<TId>
    {
        var entity = await entities.FirstOrDefaultAsync();
        if (exceptionRaiseIfNotExist && entity == null)
            throw new CanNotFoundEntityException("first entity not found.");

        if (!track)
            base.ChangeState<TEntity, TId>(entity, EntityState.Detached);

        return entity;
    }


    public async Task<TEntity> First<TEntity, TId>(Expression<Func<TEntity, bool>> search
        , string navigationPropertyPath
        , bool track = true
        , bool exceptionRaiseIfNotExist = false)
        where TId : class
        where TEntity : BaseEntity<TId>
    {
        return await First<TEntity, TId>(base.IncludeByPath<TEntity, TId>(navigationPropertyPath), search, track,
            exceptionRaiseIfNotExist);
    }


    public async Task<TEntity> First<TEntity, TId>(Expression<Func<TEntity, bool>> search
        , bool includeAllPath = false
        , bool track = true
        , bool exceptionRaiseIfNotExist = false)
        where TId : class
        where TEntity : BaseEntity<TId>
    {
        return includeAllPath
            ? await First<TEntity, TId>(base.IncludeAll<TEntity, TId>(), search, track, exceptionRaiseIfNotExist)
            : await First<TEntity, TId>(base.Query<TEntity, TId>(), search, track, exceptionRaiseIfNotExist);
    }


    public async Task<TEntity> First<TEntity, TId>(IQueryable<TEntity> entities
        , Expression<Func<TEntity, bool>> search
        , bool track = true
        , bool exceptionRaiseIfNotExist = false)
        where TId : class
        where TEntity : BaseEntity<TId>
    {
        var entity = await entities.FirstOrDefaultAsync(search);

        if (exceptionRaiseIfNotExist && entity == null)
            throw new CanNotFoundEntityException(search);


        if (!track)
            base.ChangeState<TEntity, TId>(entity, EntityState.Detached);

        return entity;
    }


    public async Task<TEntity> Get<TEntity, TId>(TId id
        , string navigationPropertyPath
        , bool track = true
        , bool exceptionRaiseIfNotExist = false)
        where TId : class
        where TEntity : BaseEntity<TId>
    {
        return await Get(base.IncludeByPath<TEntity, TId>(navigationPropertyPath), id, track, exceptionRaiseIfNotExist);
    }

    public async Task<TEntity> Get<TEntity, TId>(TId id
        , bool includeAllPath
        , bool track = true
        , bool exceptionRaiseIfNotExist = false)
        where TId : class
        where TEntity : BaseEntity<TId>
    {
        return includeAllPath
            ? await Get(base.IncludeAll<TEntity, TId>(), id, track, exceptionRaiseIfNotExist)
            : await Get(base.Query<TEntity, TId>(), id, track, exceptionRaiseIfNotExist);
    }

    public async Task<TEntity> Get<TEntity, TId>(IQueryable<TEntity> entities
        , TId id
        , bool track = true
        , bool exceptionRaiseIfNotExist = false)
        where TId : class
        where TEntity : BaseEntity<TId>
    {
        var entity = await entities.SingleOrDefaultAsync(e => e.Id == id);

        if (exceptionRaiseIfNotExist && entities == null)
            throw new CanNotFoundEntityException(id);

        if (!track)
            base.ChangeState<TEntity, TId>(entity, EntityState.Detached);

        return entity;
    }


    public async Task<TEntity> Get<TEntity, TId>(Expression<Func<TEntity, bool>> predicate
        , string navigationPropertyPath
        , bool track = true
        , bool exceptionRaiseIfNotExist = false)
        where TId : class
        where TEntity : BaseEntity<TId>
    {
        return await Get<TEntity, TId>(base.IncludeByPath<TEntity, TId>(navigationPropertyPath), predicate, track,
            exceptionRaiseIfNotExist);
    }

    public async Task<TEntity> Get<TEntity, TId>(Expression<Func<TEntity, bool>> predicate
        , bool includeAllPath
        , bool track = true
        , bool exceptionRaiseIfNotExist = false)
        where TId : class
        where TEntity : BaseEntity<TId>
    {
        return includeAllPath
            ? await Get<TEntity, TId>(base.IncludeAll<TEntity, TId>(), predicate, track, exceptionRaiseIfNotExist)
            : await Get<TEntity, TId>(base.Query<TEntity, TId>(), predicate, track, exceptionRaiseIfNotExist);
    }

    public async Task<TEntity> Get<TEntity, TId>(IQueryable<TEntity> entities
        , Expression<Func<TEntity, bool>> predicate
        , bool track = true
        , bool exceptionRaiseIfNotExist = false)
        where TId : class
        where TEntity : BaseEntity<TId>
    {
        var entity = await entities.SingleOrDefaultAsync(predicate);

        if (exceptionRaiseIfNotExist && entity == null)
            throw new CanNotFoundEntityException(predicate.ToString());

        if (!track)
            base.ChangeState<TEntity, TId>(entity, EntityState.Detached);

        return entity;
    }


    public async Task<IEnumerable<TEntity>> GetAll<TEntity, TId, TKey>(string navigationPropertyPath
        , int? page = null
        , int perPage = Pagination.PER_PAGE
        , Func<TEntity, TKey> orderBy = null
        , OrderByDirection orderbyDirection = OrderByDirection.Ascending
        , bool track = false)
        where TId : class
        where TEntity : BaseEntity<TId>
    {
        return await GetAll<TEntity, TId, TKey>(base.IncludeByPath<TEntity, TId>(navigationPropertyPath), page, perPage,
            orderBy, orderbyDirection, track);
    }


    public async Task<IEnumerable<TEntity>> GetAll<TEntity, TId, TKey>(bool includeAllPath
        , int? page = null
        , int perPage = Pagination.PER_PAGE
        , Func<TEntity, TKey> orderBy = null
        , OrderByDirection orderbyDirection = OrderByDirection.Ascending
        , bool track = false)
        where TId : class
        where TEntity : BaseEntity<TId>
    {
        return includeAllPath
            ? await GetAll<TEntity, TId, TKey>(base.IncludeAll<TEntity, TId>(), page, perPage, orderBy,
                orderbyDirection, track)
            : await GetAll<TEntity, TId, TKey>(base.Query<TEntity, TId>(), page, perPage, orderBy, orderbyDirection,
                track);
    }


    public async Task<IEnumerable<TEntity>> GetAll<TEntity, TId, TKey>(IQueryable<TEntity> entities
        , int? page = null
        , int perPage = Pagination.PER_PAGE
        , Func<TEntity, TKey> orderBy = null
        , OrderByDirection orderbyDirection = OrderByDirection.Ascending
        , bool track = false)
        where TId : class
        where TEntity : BaseEntity<TId>
    {
        IEnumerable<TEntity> data;

        if (!track)
            entities = entities.AsNoTracking();

        if (page == null)
        {
            data = entities.ToList();
        }
        else
        {
            PaginationUtils.Paginate(out var skip, perPage, page);
            data = await entities.Skip(skip).Take(perPage).ToListAsync();
        }


        if (orderBy != null)
            data = data.OrderBy(orderBy, orderbyDirection).ToList();

        return data;
    }


    public async Task<IEnumerable<TEntity>> GetByIds<TEntity, TId, TKey>(IEnumerable<TId> ids
        , string navigationPropertyPath
        , int? page = null
        , int perPage = Pagination.PER_PAGE
        , Func<TEntity, TKey> orderBy = null
        , OrderByDirection orderbyDirection = OrderByDirection.Ascending
        , bool track = false)
        where TId : class
        where TEntity : BaseEntity<TId>
    {
        return await GetByIds(base.IncludeByPath<TEntity, TId>(navigationPropertyPath), ids, page, perPage, orderBy,
            orderbyDirection, track);
    }

    public async Task<IEnumerable<TEntity>> GetByIds<TEntity, TId, TKey>(IEnumerable<TId> ids
        , bool includeAllPath
        , int? page = null
        , int perPage = Pagination.PER_PAGE
        , Func<TEntity, TKey> orderBy = null
        , OrderByDirection orderbyDirection = OrderByDirection.Ascending
        , bool track = false)
        where TId : class
        where TEntity : BaseEntity<TId>
    {
        return includeAllPath
            ? await GetByIds(base.IncludeAll<TEntity, TId>(), ids, page, perPage, orderBy, orderbyDirection, track)
            : await GetByIds(base.Query<TEntity, TId>(), ids, page, perPage, orderBy, orderbyDirection, track);
    }


    public async Task<IEnumerable<TEntity>> GetByIds<TEntity, TId, TKey>(IQueryable<TEntity> entities
        , IEnumerable<TId> ids
        , int? page = null
        , int perPage = Pagination.PER_PAGE
        , Func<TEntity, TKey> orderBy = null
        , OrderByDirection orderbyDirection = OrderByDirection.Ascending
        , bool track = false)
        where TId : class
        where TEntity : BaseEntity<TId>
    {
        _ = ids ?? throw new ArgumentNullException("ids");

        if (!track)
            entities = entities.AsNoTracking();

        var data = await ids.ForEach(async id =>
        {
            var entity = await Get(entities, id, track);
            return entity;
        });

        if (page == null)
        {
            data = entities.ToList();
        }
        else
        {
            PaginationUtils.Paginate(out var skip, perPage, page);
            data = data.Skip(skip).Take(perPage).ToList();
        }

        if (orderBy != null)
            data = data.OrderBy(orderBy, orderbyDirection).ToList();

        return data;
    }


    public async Task<IEnumerable<TEntity>> Where<TEntity, TId, TKey>(Expression<Func<TEntity, bool>> search
        , string navigationPropertyPath
        , int? page = null
        , int perPage = Pagination.PER_PAGE
        , Func<TEntity, TKey> orderBy = null
        , OrderByDirection orderbyDirection = OrderByDirection.Ascending
        , bool track = false)
        where TId : class
        where TEntity : BaseEntity<TId>
    {
        return await Where<TEntity, TId, TKey>(base.IncludeByPath<TEntity, TId>(navigationPropertyPath), search, page,
            perPage, orderBy, orderbyDirection, track);
    }


    public async Task<IEnumerable<TEntity>> Where<TEntity, TId, TKey>(Expression<Func<TEntity, bool>> search
        , bool includeAllPath
        , int? page = null
        , int perPage = Pagination.PER_PAGE
        , Func<TEntity, TKey> orderBy = null
        , OrderByDirection orderbyDirection = OrderByDirection.Ascending
        , bool track = false)
        where TId : class
        where TEntity : BaseEntity<TId>
    {
        return includeAllPath
            ? await Where<TEntity, TId, TKey>(base.IncludeAll<TEntity, TId>(), search, page, perPage, orderBy,
                orderbyDirection, track)
            : await Where<TEntity, TId, TKey>(base.Query<TEntity, TId>(), search, page, perPage, orderBy,
                orderbyDirection, track);
    }


    public async Task<IEnumerable<TEntity>> Where<TEntity, TId, TKey>(IQueryable<TEntity> entities
        , Expression<Func<TEntity, bool>> search
        , int? page = null
        , int perPage = Pagination.PER_PAGE
        , Func<TEntity, TKey> orderBy = null
        , OrderByDirection orderbyDirection = OrderByDirection.Ascending
        , bool track = false)
        where TId : class
        where TEntity : BaseEntity<TId>
    {
        await Task.CompletedTask;

        if (!track)
            entities = entities.AsNoTracking();

        entities = entities.Where(search);


        var data = default(IEnumerable<TEntity>);

        if (orderBy != null)
            data = entities.OrderBy(orderBy, orderbyDirection);

        if (page != null)
        {
            PaginationUtils.Paginate(out var skip, perPage, page);
            data = data.Skip(skip).Take(perPage).ToList();
        }

        return data.ToList();
    }


    public async Task<int> Count<TEntity, TId>(Expression<Func<TEntity, bool>> where, string navigationPropertyPath)
        where TId : class
        where TEntity : BaseEntity<TId>
    {
        return await Count<TEntity, TId>(base.IncludeByPath<TEntity, TId>(navigationPropertyPath), where);
    }

    public async Task<int> Count<TEntity, TId>(Expression<Func<TEntity, bool>> where)
        where TId : class
        where TEntity : BaseEntity<TId>
    {
        return await Count<TEntity, TId>(base.Query<TEntity, TId>(), where);
    }


    public async Task<int> Count<TEntity, TId>(IQueryable<TEntity> entities, Expression<Func<TEntity, bool>> where)
        where TId : class
        where TEntity : BaseEntity<TId>
    {
        var result = await entities.Where(where).CountAsync();
        return result;
    }

    public async Task<int> Count<TEntity, TId>()
        where TId : class
        where TEntity : BaseEntity<TId>
    {
        var result = await base.Query<TEntity, TId>().CountAsync();
        return result;
    }
}