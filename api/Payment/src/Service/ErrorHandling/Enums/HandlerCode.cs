namespace ErrorHandling.Enums;

public enum HandlerCode
{
    CreatePaymentMethod = 11_01,
    DeletePaymentMethod = 11_02,
    UpdatePaymentMethod = 11_03,
    GetPaymentMethodList = 11_04,

    CreateCurrency = 12_01,
    DeleteCurrency = 12_02,
    UpdateCurrency = 12_03,
    GetCurrencyList = 12_04,

    CreatePayment = 13_01,
    VerifyPayment = 13_02,
    CancelPayment = 13_03,
    PaymentList = 13_04,
    PaymentDetail = 13_05,
    PaymentVisualize = 13_06,

    CustomerList = 14_01,

    TransactionList = 15_01,

    PayerList = 16_01,

    PaymentStatusList = 17_01,

    RegisterApplication = 18_01,
    GetApplicationsList = 18_02
}