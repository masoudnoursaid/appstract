using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using ApplicationEntity = Domain.Entities.Application;

namespace Infrastructure.Persistence.Sql;

public interface IPaymentDbContext
{
    DbSet<PaymentMethod> PaymentMethods { get; set; }
    DbSet<Currency> Currencies { get; set; }
    DbSet<MerchantOwner> MerchantOwners { get; set; }
    DbSet<Payment> Payments { get; set; }
    DbSet<Transaction> Transactions { get; set; }
    DbSet<Payer> Payers { get; set; }
    DbSet<PaymentStatus> PaymentStatus { get; set; }
    DbSet<ApplicationEntity> Applications { get; set; }
}