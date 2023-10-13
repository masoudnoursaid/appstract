using Microsoft.AspNetCore.Mvc;

namespace Global.Mvc.Common.Utils;

public class VisualizeControllerUtils
{
    public static RedirectResult VisualizeResult(string paymentId)
    {
        return new RedirectResult(Constants.EndPoints.VisualizeController.PAYMENT_RESULT + $"?paymentId={paymentId}");
    }
    public static RedirectResult VisualizeCancel(string paymentId)
    {
        return new RedirectResult(Constants.EndPoints.VisualizeController.PAYMENT_CANCEL + $"?paymentId={paymentId}");
    }
}