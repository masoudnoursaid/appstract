using Application.Repositories.Generic;
using Application.Repositories.Generic.Availability;
using Application.Repositories.Generic.Create;
using Application.Repositories.Generic.Delete;
using Application.Repositories.Generic.Read;
using Application.Repositories.Generic.Update;
using Domain.Common.BaseTypes;

namespace Infrastructure.Persistence.Sql.GenericRepository;

public class EntityRepository<TEntity> : EntityRepository<TEntity, string>, IEntityRepository<TEntity>
    where TEntity : BaseEntity<string>
{
    public EntityRepository(IReadRepository<TEntity, string> readRepository
        , ICreateRepository<TEntity, string> createRepository
        , IDeleteRepository<TEntity, string> deleteRepository
        , IAvailabilityRepository<TEntity, string> availabilityRepository
        , IUpdateRepository<TEntity, string> updateRepository)
        : base(readRepository, createRepository, deleteRepository, availabilityRepository, updateRepository)
    {
    }
}