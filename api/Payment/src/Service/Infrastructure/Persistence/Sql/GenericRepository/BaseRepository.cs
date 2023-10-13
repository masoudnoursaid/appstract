using Domain.Common.BaseTypes;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Sql.GenericRepository;

public abstract class BaseRepository<TContext> : IDisposable
    where TContext : DbContext
{
    private readonly TContext _context;


    private bool _disposed;

    public BaseRepository(TContext context)
    {
        _context = context;
    }

    public void Dispose()
    {
        if (!_disposed)
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
            _disposed = !_disposed;
        }
    }


    public virtual DbSet<TEntity> Table<TEntity, TId>()
        where TId : class
        where TEntity : BaseEntity<TId>
    {
        return _context.Set<TEntity>();
    }

    public virtual IQueryable<TEntity> Query<TEntity, TId>()
        where TId : class
        where TEntity : BaseEntity<TId>
    {
        return _context.Set<TEntity>().AsQueryable();
    }


    public virtual async Task SaveChanges(CancellationToken cancellationToken = default)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }

    public virtual async Task<int> SaveChanges(bool acceptAllChangesOnSuccess,
        CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }


    public virtual IQueryable<TEntity> IncludeByPath<TEntity, TId>(string navigationPropertyPath)
        where TId : class
        where TEntity : BaseEntity<TId>
    {
        var table = _context.Set<TEntity>().AsQueryable();

        if (string.IsNullOrEmpty(navigationPropertyPath))
            return table;

        var props = navigationPropertyPath.Split(";");

        foreach (var prop in props) table = table.Include(prop);
        return table;
    }

    public virtual IQueryable<TEntity> IncludeAll<TEntity, TId>()
        where TId : class
        where TEntity : BaseEntity<TId>
    {
        var table = _context.Set<TEntity>().AsQueryable();

        var navigations = _context.Model.FindEntityType(typeof(TEntity))
            ?.GetDerivedTypesInclusive()
            .SelectMany(type => type.GetNavigations())
            .Distinct();

        foreach (var property in navigations!)
            table = table.Include(property.Name);

        return table;
    }


    public virtual void ChangeState<TEntity, TId>(TEntity entity, EntityState state)
        where TId : class
        where TEntity : BaseEntity<TId>
    {
        _context.Entry(entity).State = state;
    }
}