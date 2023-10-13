using Application.Common.BaseTypes.Dto;
using Domain.Enums;
using Domain.ValueObjects;

namespace Application.Business.PaymentMethod.Dto;

public record PaymentMethodModelDto : ModelDto
{
    public string Title { get; set; } = null!;

    public IEnumerable<GeoLocation>? GeographicSanctions { get; set; }

    public IEnumerable<GeoLocation>? SupportedCountries { get; set; }

    public int DisplayOrder { get; set; }

    public string DisplayTitle { get; set; } = null!;

    public string Provider { get; set; } = null!;

    public bool? GeographicRestrictionEnforced { get; set; }

    public string? GeographicRestriction { get; set; }

    public string? PaymentAmountsLive { get; set; }

    public string? PaymentAmountsSandbox { get; set; }
    public SourceImplementedGetWay? GetWay { get; set; }

    public bool Active { get; set; }

    public bool? Sandbox { get; set; }

    public decimal? MinValue { get; set; }

    public decimal? MaxValue { get; set; }

    public string? Icon { get; set; }

    public string? CountryIcon { get; set; }
    public string MerchantOwnerId { get; set; } = null!;
}