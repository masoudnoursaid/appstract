using Domain.Entities;
using Domain.ValueObjects;
using Infrastructure.Persistence.Sql.Configuration.Converter;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Sql.Configuration;

public class PaymentMethodConfiguration : ISqlEntityConfiguration, IEntityTypeConfiguration<PaymentMethod>
{
    public void Configure(EntityTypeBuilder<PaymentMethod> builder)
    {
        var splitStringConverter = new JsonValueConverter<IEnumerable<GeoLocation>>();

        builder.Property<IEnumerable<GeoLocation>>(nameof(PaymentMethod.GeographicSanctions))
            .HasConversion(splitStringConverter);

        builder.Property<IEnumerable<GeoLocation>>(nameof(PaymentMethod.SupportedCountries))
            .HasConversion(splitStringConverter);

        builder.HasIndex(pm => pm.Title).IsUnique();
    }
}