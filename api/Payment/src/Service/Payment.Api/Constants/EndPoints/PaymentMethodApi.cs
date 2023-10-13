using Infrastructure.Common.Consts.EndPoints;

namespace Payment.Api.Constants.EndPoints;

public abstract class PaymentMethodApi : CrudController
{
    public const string CONTROLLER = "payment-mthod";
}