using Application.Common.BaseTypes.Dto;

namespace Application.Business.Applications.Dto;

public record ApplicationDto : IDto
{
    public string? Title { get; set; }

    public IEnumerable<string>? AuthorizedIpAddresses { get; set; }

    public string? ApiKey { get; set; }
    public string? ApiSecret { get; set; }
    public string Id { get; set; } = null!;
    public DateTime CreatedDateTime { get; set; }
    public DateTime UpdatedDateTime { get; set; }
}