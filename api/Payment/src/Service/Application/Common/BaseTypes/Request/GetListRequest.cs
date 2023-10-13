using Application.Common.Consts;

namespace Application.Common.BaseTypes.Request;

public abstract record GetListRequest
{
    protected GetListRequest(int perPage = Pagination.PER_PAGE, int? page = null)
    {
        Page = page;
        PerPage = perPage;
    }

    public int PerPage { get; set; } = Pagination.PER_PAGE;
    public int? Page { get; set; }
}