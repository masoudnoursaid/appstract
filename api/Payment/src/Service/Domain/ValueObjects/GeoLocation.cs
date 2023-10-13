using Domain.Common.BaseTypes;

namespace Domain.ValueObjects;

public class GeoLocation : ValueObject
{
    public string? CountryName { get; set; }
    public string? CountryCode { get; set; }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return CountryName!;
        yield return CountryCode!;
    }
}