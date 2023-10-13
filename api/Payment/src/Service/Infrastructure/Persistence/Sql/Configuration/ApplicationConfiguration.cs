using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ApplicationEntity = Domain.Entities.Application;

namespace Infrastructure.Persistence.Sql.Configuration;

public class ApplicationConfiguration : ISqlEntityConfiguration, IEntityTypeConfiguration<ApplicationEntity>
{
    public void Configure(EntityTypeBuilder<ApplicationEntity> builder)
    {
        builder.OwnsOne(c => c.ApiKey)
            .Property(x => x.Value)
            .HasColumnName("ApiKey");


        builder.OwnsOne(c => c.ApiSecret)
            .Property(x => x.Value)
            .HasColumnName("ApiSecret");

        builder
            .Property("_authorizedIpAddresses")
            .HasColumnName("AuthorizedIpAddresses");
    }
}