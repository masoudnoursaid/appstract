using Application.Common.BaseTypes.Dto;

namespace Application.Business.Payer.Dto;

public record PayerDto : IBaseDto
{
    public string? ProvidedId { get; set; }
    public string? Email { get; set; }
    public string? CountryCode { get; set; }
    public string? City { get; set; }
    public string? Address { get; set; }
    public string? Firstname { get; set; }
    public string? Lastname { get; set; }
    public string? PostalCode { get; set; }
    public string? PaymentId { get; set; }
}