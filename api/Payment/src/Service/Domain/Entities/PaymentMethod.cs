using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common.BaseTypes;
using Domain.Enums;
using Domain.ValueObjects;

namespace Domain.Entities;

public class PaymentMethod : BaseEntity
{
    public string Title { get; set; } = null!;

    public int DisplayOrder { get; set; }

    public string DisplayTitle { get; set; } = null!;

    public string Provider { get; set; } = null!;

    public bool? GeographicRestrictionEnforced { get; set; }

    public bool Active { get; set; }

    public bool? Sandbox { get; set; }

    public decimal? MinValue { get; set; }

    public decimal? MaxValue { get; set; }
    public string? Icon { get; set; }

    public SourceImplementedGetWay GetWay { get; set; }


    public IEnumerable<GeoLocation> GeographicSanctions { get; set; } = new List<GeoLocation>();
    public IEnumerable<GeoLocation> SupportedCountries { get; set; } = new List<GeoLocation>();


    [ForeignKey(nameof(MerchantOwner))] public string MerchantOwnerId { get; set; } = null!;

    public MerchantOwner MerchantOwner { get; set; } = null!;


    public ICollection<Payment>? Payments { get; set; }

}