using System.Linq.Expressions;
using Domain.Common.BaseTypes;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Sql.GenericRepository.Create;

public class BaseCreateRepository<TContext> : BaseRepository<TContext>
    where TContext : DbContext
{
    private readonly TContext _context;

    public BaseCreateRepository(TContext context) : base(context)
    {
        _context = context;
    }

    public async Task ClearAllEntitiesThenAddRange<TEntity, TId>(IEnumerable<TEntity> insertEntities)
        where TId : class
        where TEntity : BaseEntity<TId>
    {
        var all = base.Table<TEntity, TId>().ToList();
        base.Table<TEntity, TId>().RemoveRange(all);
        var baseEntities = insertEntities.ToList();
        if (baseEntities.Any())
            await base.Table<TEntity, TId>().AddRangeAsync(baseEntities);
        await _context.SaveChangesAsync();
    }


    public async Task ClearRemoveListThenAddRange<TEntity, TId>(IEnumerable<TEntity> removeList,
        IEnumerable<TEntity> insertEntities)
        where TId : class
        where TEntity : BaseEntity<TId>
    {
        base.Table<TEntity, TId>().RemoveRange(removeList);
        var baseEntities = insertEntities.ToList();
        if (baseEntities.Any())
            await base.Table<TEntity, TId>().AddRangeAsync(baseEntities);
        await _context.SaveChangesAsync();
    }


    public async Task Insert<TEntity, TId>(TEntity entity)
        where TId : class
        where TEntity : BaseEntity<TId>
    {
        await base.Table<TEntity, TId>().AddAsync(entity);
        await _context.SaveChangesAsync();
    }


    public async Task InsertRange<TEntity, TId>(IEnumerable<TEntity> entities)
        where TId : class
        where TEntity : BaseEntity<TId>
    {
        await base.Table<TEntity, TId>().AddRangeAsync(entities);
        await _context.SaveChangesAsync();
    }


    public async Task ReCreate<TEntity, TId>(Expression<Func<TEntity, bool>> deleteCondition,
        IEnumerable<TEntity> insertEntities)
        where TId : class
        where TEntity : BaseEntity<TId>
    {
        var all = base.Table<TEntity, TId>().Where(deleteCondition).ToList();
        base.Table<TEntity, TId>().RemoveRange(all);
        await base.Table<TEntity, TId>().AddRangeAsync(insertEntities);
        await _context.SaveChangesAsync();
    }
}