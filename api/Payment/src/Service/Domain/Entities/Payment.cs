using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common.BaseTypes;

namespace Domain.Entities;

public class Payment : BaseEntity
{
    public string? ProvidedId { get; set; }
    public float Amount { get; set; }
    public string ClientRedirectUrl { get; set; } = null!;
    public string ClientWebHookUrl { get; set; } = null!;


    [ForeignKey(nameof(PaymentMethod))] public string? PaymentMethodId { get; set; }
    public PaymentMethod? PaymentMethod { get; set; }


    [ForeignKey(nameof(Payer))] public string? PayerId { get; set; }
    public Payer? Payer { get; set; }


    [ForeignKey(nameof(Status))] public string StatusId { get; set; } = null!;

    public PaymentStatus Status { get; set; } = null!;


    [ForeignKey(nameof(Currency))] public string CurrencyId { get; set; } = null!;
    public Currency Currency { get; set; } = null!;


    [ForeignKey(nameof(Transaction))] public string? TransactionId { get; set; }
    public Transaction? Transaction { get; set; }

    [ForeignKey(nameof(Customer))] public string? CustomerId { get; set; }
    public Customer? Customer { get; set; }

    public DateTime? CompletedDate { get; set; }

    [ForeignKey(nameof(Application))] public string ApplicationId { get; set; } = null!;
    public Application Application { get; set; } = null!;
}