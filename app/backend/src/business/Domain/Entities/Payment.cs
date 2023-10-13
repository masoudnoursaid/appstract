using Domain.Common.BaseTypes;

namespace Domain.Entities;

public class Payment : BaseEntity
{
    /// <summary>
    /// Represent provider unique id for this payment
    /// Current supported providers : papal, stripe, billplz, mollie
    /// </summary>
    public string ProvidedId { get; set; } = null!;
    
    /// <summary>
    /// Represent payhub payment id
    /// </summary>
    public string PayHubPaymentId { get; set; } = null!;
    
    /// <summary>
    /// Represent payment method id, which is a valid id in payhub
    /// </summary>
    public string PaymentMethodId { get; set; } = null!;
    
    /// <summary>
    /// Represent the amount of pay for current payment
    /// </summary>
    public float Amount { get; set; }
}