using ErrorHandling.Attributes;
using ErrorHandling.Enums;

namespace Application.Business.Currency.Commands.UpdateCurrency;

[HandlerCode(HandlerCode.UpdateCurrency)]
public enum UpdateCurrencyErrorCodes
{
    CurrencyNotFound = 12_03_01
}