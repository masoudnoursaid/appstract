using System.Diagnostics.CodeAnalysis;

namespace Appstract.Front.Application.Common.Constants;

[ExcludeFromCodeCoverage]
public static class Routes
{
    public const string LOGIN = "login";
    public const string PERSONAL_INFO = "personal-info";
    public const string DASHBOARD = "dashboard";
    public const string GET_INFO = "get-info";
    public const string FINANCIAL = "financial";
    public const string BUY_CREDIT = $"{FINANCIAL}/buy-credit";
    public const string TOP_UP_VOUCHER = $"{FINANCIAL}/topup-voucher";
    public const string PURCHASE_HISTORY = $"{FINANCIAL}/purchase-history";


}