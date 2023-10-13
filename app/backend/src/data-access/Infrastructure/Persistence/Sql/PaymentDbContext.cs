using System.Reflection;
using Infrastructure.Persistence.Sql.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Sql;

public class AppstractDbContext : DbContext, IAppstractDbContext
{
    public AppstractDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<PaymentEntity> Payments { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(
            Assembly.GetExecutingAssembly(),
            type => type.Namespace == typeof(ISqlEntityConfiguration).Namespace);
    }
}