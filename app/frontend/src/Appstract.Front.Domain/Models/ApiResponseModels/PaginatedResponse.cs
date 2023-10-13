namespace Appstract.Front.Domain.Models.ApiResponseModels;

public class PaginatedResponse<TResult>
    where TResult : new()
{
    public List<TResult> Items { get; set; } = new();
    public int TotalCount { get; set; }
}