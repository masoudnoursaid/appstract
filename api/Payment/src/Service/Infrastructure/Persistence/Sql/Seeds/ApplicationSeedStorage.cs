using Domain.ValueObjects;

namespace Infrastructure.Persistence.Sql.Seeds;

public static class ApplicationSeedStorage
{
    public static readonly ApiKey Key = new("NL");
    public static readonly ApiSecret Secret = new();
    public static readonly string Id = Guid.NewGuid().ToString();
}