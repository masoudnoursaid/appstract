using Application.Repositories.Generic.Availability;
using Application.Repositories.Generic.Create;
using Application.Repositories.Generic.Delete;
using Application.Repositories.Generic.Read;
using Application.Repositories.Generic.Update;
using Domain.Common.BaseTypes;

namespace Application.Repositories.Generic;

public interface IEntityRepository<TEntity> : IEntityRepository<TEntity, string>
    , ICreateRepository<TEntity>
    , IDeleteRepository<TEntity>
    , IUpdateRepository<TEntity>
    , IAvailabilityRepository<TEntity>
    where TEntity : BaseEntity<string>
{
}

public interface IEntityRepository<TEntity, TId> : IReadRepository<TEntity, TId>
    , ICreateRepository<TEntity, TId>
    , IDeleteRepository<TEntity, TId>
    , IUpdateRepository<TEntity, TId>
    , IAvailabilityRepository<TEntity, TId>
    where TId : class
    where TEntity : BaseEntity<TId>
{
}