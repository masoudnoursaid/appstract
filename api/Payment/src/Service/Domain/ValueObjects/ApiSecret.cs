using System.ComponentModel.DataAnnotations;
using Domain.Common.BaseTypes;
using Domain.Common.Consts;
using Domain.Common.Hash;
using Domain.Common.Util;

namespace Domain.ValueObjects;

public sealed class ApiSecret : ValueObject
{
    public ApiSecret()
    {
        var date = DateTime.UtcNow;
        var guid = Guid.NewGuid() + date.Ticks.ToString() + "Payment-API-SECRET";
        var hash = Sha256.HashStream(StringUtils.GenerateStreamFromString(guid));
        Value = ArrayUtils.ArrayToString(hash);
    }

    [MaxLength(PropertyLength.SHA256_TOKEN_MAX_LENGTH)]
    public string Value { get; set; }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}