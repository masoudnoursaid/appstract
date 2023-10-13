namespace Appstract.Front.Domain.Models.ApiRequestModels;

public class Pagination
{
    public Pagination()
    {
    }

    public Pagination(int pageNumber, int pageSize)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
    }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}