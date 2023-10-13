namespace Application.Common.Attributes;

/// <summary>
///     custom attribute to catch in reflection queries
/// </summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Class | AttributeTargets.Field)]
public class ExcludeAttribute : Attribute
{
    public string? Reason { get; set; }
}