using Domain.Common.BaseTypes;

namespace Domain.Entities;

public class MerchantOwner : BaseEntity
{
    public string? Email { get; set; }
    public string? Name { get; set; }
}