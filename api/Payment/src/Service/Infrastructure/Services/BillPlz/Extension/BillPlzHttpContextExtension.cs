using Infrastructure.Common.Extensions;
using Infrastructure.Services.BillPlz.Model;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Services.BillPlz.Extension;

public static class BillPlzHttpContextExtension
{
    public static BillPlzHmacModel GetBillPlzHmacModel(this HttpContext context)
    {
        var result = context.ConvertFromQueryString<BillPlzHmacModel>();
        return result;
    }
}