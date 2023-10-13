using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Sql;

public interface IAppstractDbContext
{
    DbSet<PaymentEntity> Payments { get; set; }
}