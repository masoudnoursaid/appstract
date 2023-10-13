using Domain.Entities;
using Domain.ValueObjects.Payment;
using Infrastructure.Persistence.Sql.Configuration.Converter;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Sql.Configuration;

public class TransactionConfiguration : ISqlEntityConfiguration, IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        var splitStringConverter = new JsonValueConverter<IEnumerable<PaymentItem>>();
        builder.Property<IEnumerable<PaymentItem>>(nameof(Transaction.Items)).HasConversion(splitStringConverter);
    }
}