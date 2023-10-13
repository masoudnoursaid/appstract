using ErrorHandling.Attributes;
using ErrorHandling.Enums;

namespace Application.Business.Currency.Commands.DeleteCurrency;

[HandlerCode(HandlerCode.DeleteCurrency)]
public enum DeleteCurrencyErrorCodes
{
    CurrencyNotFound = 12_02_01
}