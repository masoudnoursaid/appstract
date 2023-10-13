using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common.BaseTypes;

namespace Domain.Entities;

public class Payer : BaseEntity
{
    public string? ProvidedId { get; set; }
    public string? Email { get; set; }
    public string? CountryCode { get; set; }
    public string? City { get; set; }
    public string? Address { get; set; }
    public string? Firstname { get; set; }
    public string? Lastname { get; set; }
    public string? PostalCode { get; set; }

    [ForeignKey(nameof(Payment))] public string? PaymentId { get; set; }

    public Payment? Payment { get; set; }
}