using System.Reflection;
using Domain.Entities;
using Infrastructure.Persistence.Sql.Configuration;
using Microsoft.EntityFrameworkCore;
using ApplicationEntity = Domain.Entities.Application;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

namespace Infrastructure.Persistence.Sql;

public class PaymentDbContext : DbContext, IPaymentDbContext
{
    public PaymentDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<PaymentMethod> PaymentMethods { get; set; } = null!;
    public DbSet<Currency> Currencies { get; set; } = null!;
    public DbSet<MerchantOwner> MerchantOwners { get; set; } = null!;
    public DbSet<Payment> Payments { get; set; } = null!;
    public DbSet<Transaction> Transactions { get; set; } = null!;
    public DbSet<Payer> Payers { get; set; } = null!;
    public DbSet<PaymentStatus> PaymentStatus { get; set; } = null!;
    public DbSet<ApplicationEntity> Applications { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(
            Assembly.GetExecutingAssembly(),
            type => type.Namespace == typeof(ISqlEntityConfiguration).Namespace);
    }
}