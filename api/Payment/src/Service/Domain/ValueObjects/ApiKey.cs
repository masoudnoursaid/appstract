using System.ComponentModel.DataAnnotations;
using Domain.Common.BaseTypes;
using Domain.Common.Consts;
using Domain.Common.Hash;
using Domain.Common.Util;

namespace Domain.ValueObjects;

public sealed class ApiKey : ValueObject
{
    public ApiKey(string region = "")
    {
        var date = DateTime.UtcNow;
        var guid = Guid.NewGuid() + date.Ticks.ToString() + "Payment-API-KEY";
        var hash = Sha256.HashStream(StringUtils.GenerateStreamFromString(guid));
        var output = ArrayUtils.ArrayToString(hash);
        Value = $"{region}_{output}";
    }

    public ApiKey()
    {
        Value = default!;
    }


    [MaxLength(PropertyLength.SHA256_TOKEN_MAX_LENGTH)]
    public string Value { get; set; }


    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}