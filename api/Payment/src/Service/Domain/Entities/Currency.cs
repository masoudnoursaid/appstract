using Domain.Common.BaseTypes;

namespace Domain.Entities;

public class Currency : BaseEntity
{
    public string Title { get; set; } = null!;

    public string FullName { get; set; } = null!;

    public string Symbol { get; set; } = null!;
}