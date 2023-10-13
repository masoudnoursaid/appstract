using System.Linq.Expressions;
using Application.Common.Consts;
using Application.Repositories.Generic.Read;
using Domain.Common.BaseTypes;
using Microsoft.EntityFrameworkCore;
using MoreLinq;

#pragma warning disable CS8625

namespace Infrastructure.Persistence.Sql.GenericRepository.Read;

public class ReadRepository<TEntity, TContext> : ReadRepository<TEntity, string, TContext>, IReadRepository<TEntity>
    where TContext : DbContext
    where TEntity : BaseEntity<string>
{
    public ReadRepository(TContext context) : base(context)
    {
    }
}

public class ReadRepository<TEntity, TId, TContext> : BaseReadRepository<TContext>, IReadRepository<TEntity, TId>
    where TId : class
    where TContext : DbContext
    where TEntity : BaseEntity<TId>
{
    public ReadRepository(TContext context) : base(context)
    {
    }


    public async Task<TEntity> First(string navigationPropertyPath
        , bool track = true
        , bool exceptionRaiseIfNotExist = false)
    {
        return await First<TEntity, TId>(navigationPropertyPath, track, exceptionRaiseIfNotExist);
    }

    public async Task<TEntity> First(bool includeAllPath = false
        , bool track = true
        , bool exceptionRaiseIfNotExist = false)
    {
        return await First<TEntity, TId>(includeAllPath, track, exceptionRaiseIfNotExist);
    }

    public async Task<TEntity> First(IQueryable<TEntity> entities
        , bool track = true
        , bool exceptionRaiseIfNotExist = false)
    {
        return await First<TEntity, TId>(entities, track, exceptionRaiseIfNotExist);
    }


    public async Task<TEntity> First(Expression<Func<TEntity, bool>> search
        , string navigationPropertyPath
        , bool track = true
        , bool exceptionRaiseIfNotExist = false)
    {
        return await First<TEntity, TId>(search, navigationPropertyPath, track, exceptionRaiseIfNotExist);
    }

    public async Task<TEntity> First(Expression<Func<TEntity, bool>> search
        , bool includeAllPath = false
        , bool track = true
        , bool exceptionRaiseIfNotExist = false)
    {
        return await First<TEntity, TId>(search, includeAllPath, track, exceptionRaiseIfNotExist);
    }

    public async Task<TEntity> First(IQueryable<TEntity> entities
        , Expression<Func<TEntity, bool>> search
        , bool track = true
        , bool exceptionRaiseIfNotExist = false)
    {
        return await First<TEntity, TId>(entities, search, track, exceptionRaiseIfNotExist);
    }


    public async Task<TEntity> Get(TId key
        , string navigationPropertyPath
        , bool track = true
        , bool exceptionRaiseIfNotExist = false)
    {
        return await Get<TEntity, TId>(key, navigationPropertyPath, track, exceptionRaiseIfNotExist);
    }

    public async Task<TEntity> Get(TId key
        , bool includeAllPath
        , bool track = true
        , bool exceptionRaiseIfNotExist = false)
    {
        return await Get<TEntity, TId>(key, includeAllPath, track, exceptionRaiseIfNotExist);
    }

    public async Task<TEntity> Get(IQueryable<TEntity> entities
        , TId key
        , bool track = true
        , bool exceptionRaiseIfNotExist = false)
    {
        return await base.Get(entities, key, track, exceptionRaiseIfNotExist);
    }


    public async Task<TEntity> Get(Expression<Func<TEntity, bool>> predicate
        , string navigationPropertyPath
        , bool track = true
        , bool exceptionRaiseIfNotExist = false)
    {
        return await Get<TEntity, TId>(predicate, navigationPropertyPath, track, exceptionRaiseIfNotExist);
    }

    public async Task<TEntity> Get(Expression<Func<TEntity, bool>> predicate
        , bool includeAllPath
        , bool track = true
        , bool exceptionRaiseIfNotExist = false)
    {
        return await Get<TEntity, TId>(predicate, includeAllPath, track, exceptionRaiseIfNotExist);
    }


    public async Task<TEntity> Get(IQueryable<TEntity> entities
        , Expression<Func<TEntity, bool>> predicate
        , bool track = true
        , bool exceptionRaiseIfNotExist = false)
    {
        return await Get<TEntity, TId>(entities, predicate, track, exceptionRaiseIfNotExist);
    }


    public async Task<IEnumerable<TEntity>> GetAll<TKey>(
        string navigationPropertyPath
        , int? page = null
        , int perPage = Pagination.PER_PAGE
        , Func<TEntity, TKey> orderBy = null
        , OrderByDirection orderbyDirection = OrderByDirection.Ascending
        , bool track = false)
    {
        return await GetAll<TEntity, TId, TKey>(navigationPropertyPath, page, perPage, orderBy, orderbyDirection,
            track);
    }


    public async Task<IEnumerable<TEntity>> GetAll<TKey>(
        bool includeAllPath
        , int? page = null
        , int perPage = Pagination.PER_PAGE
        , Func<TEntity, TKey> orderBy = null
        , OrderByDirection orderbyDirection = OrderByDirection.Ascending
        , bool track = false)
    {
        return await GetAll<TEntity, TId, TKey>(includeAllPath, page, perPage, orderBy, orderbyDirection, track);
    }

    public async Task<IEnumerable<TEntity>> GetAll(
        bool includeAllPath = false
        , int? page = null
        , int perPage = Pagination.PER_PAGE
        , bool track = false)
    {
        return await GetAll<object>(includeAllPath, page, perPage, track: track);
    }


    public async Task<IEnumerable<TEntity>> GetAll<TKey>(IQueryable<TEntity> entities
        , int? page = null
        , int perPage = Pagination.PER_PAGE
        , Func<TEntity, TKey> orderBy = null
        , OrderByDirection orderbyDirection = OrderByDirection.Ascending
        , bool track = false)
    {
        return await GetAll<TEntity, TId, TKey>(entities, page, perPage, orderBy, orderbyDirection, track);
    }


    public async Task<IEnumerable<TEntity>> GetByIds<TKey>(IEnumerable<TId> ids
        , string navigationPropertyPath
        , int? page = null
        , int perPage = Pagination.PER_PAGE
        , Func<TEntity, TKey> orderBy = null
        , OrderByDirection orderbyDirection = OrderByDirection.Ascending
        , bool track = false)
    {
        return await base.GetByIds(ids, navigationPropertyPath, page, perPage, orderBy, orderbyDirection, track);
    }

    public async Task<IEnumerable<TEntity>> GetByIds<TKey>(IEnumerable<TId> ids
        , bool includeAllPath
        , int? page = null
        , int perPage = Pagination.PER_PAGE
        , Func<TEntity, TKey> orderBy = null
        , OrderByDirection orderbyDirection = OrderByDirection.Ascending
        , bool track = false)
    {
        return await base.GetByIds(ids, includeAllPath, page, perPage, orderBy, orderbyDirection, track);
    }


    public async Task<IEnumerable<TEntity>> GetByIds<TKey>(IQueryable<TEntity> entities
        , IEnumerable<TId> ids
        , int? page = null
        , int perPage = Pagination.PER_PAGE
        , Func<TEntity, TKey> orderBy = null
        , OrderByDirection orderbyDirection = OrderByDirection.Ascending
        , bool track = false)
    {
        return await base.GetByIds(entities, ids, page, perPage, orderBy, orderbyDirection, track);
    }


    public async Task<IEnumerable<TEntity>> Where<TKey>(Expression<Func<TEntity, bool>> search
        , string navigationPropertyPath
        , int? page = null
        , int perPage = Pagination.PER_PAGE
        , Func<TEntity, TKey> orderBy = null
        , OrderByDirection orderbyDirection = OrderByDirection.Ascending
        , bool track = false)
    {
        return await Where<TEntity, TId, TKey>(search, navigationPropertyPath, page, perPage, orderBy,
            orderbyDirection, track);
    }


    public async Task<IEnumerable<TEntity>> Where<TKey>(Expression<Func<TEntity, bool>> search
        , bool includeAllPath
        , int? page = null
        , int perPage = Pagination.PER_PAGE
        , Func<TEntity, TKey> orderBy = null
        , OrderByDirection orderbyDirection = OrderByDirection.Ascending
        , bool track = false)
    {
        return await Where<TEntity, TId, TKey>(search, includeAllPath, page, perPage, orderBy, orderbyDirection,
            track);
    }


    public async Task<IEnumerable<TEntity>> Where<TKey>(IQueryable<TEntity> entities
        , Expression<Func<TEntity, bool>> search
        , int? page = null
        , int perPage = Pagination.PER_PAGE
        , Func<TEntity, TKey> orderBy = null
        , OrderByDirection orderbyDirection = OrderByDirection.Ascending
        , bool track = false)
    {
        return await Where<TEntity, TId, TKey>(entities, search, page, perPage, orderBy, orderbyDirection, track);
    }


    public async Task<int> Count(Expression<Func<TEntity, bool>> where, string navigationPropertyPath)
    {
        return await Count<TEntity, TId>(where, navigationPropertyPath);
    }

    public async Task<int> Count(Expression<Func<TEntity, bool>> where)
    {
        return await Count<TEntity, TId>(where);
    }


    public async Task<int> Count(IQueryable<TEntity> entities, Expression<Func<TEntity, bool>> where)
    {
        return await Count<TEntity, TId>(entities, where);
    }

    public async Task<int> Count()
    {
        return await Count<TEntity, TId>();
    }
}