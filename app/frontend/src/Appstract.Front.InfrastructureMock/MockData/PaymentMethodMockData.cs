using Appstract.Front.Domain.Models.Financial;

namespace Appstract.Web.Infrastructure.MockData;

public static class PaymentMethodMockData
{
    public static Task<List<PaymentMethod>> GetPaymentMethods()
    {
          return Task.FromResult(new List<PaymentMethod>
        {
            new()
            {
                PaymentMethodId = 5,
                CountryIcon = "img/payments/flags/global.png",
                Provider = "Stripe",
                DisplayTitle = "Credit Card",
                Currency = "USD",
                MinAmount = 20.00m,
                MaxAmount = 50.00m,
                Amounts = new List<long> { 20, 50 },
                Icon = new List<string>
                {
                    "img/payments/gateways/mastercard.png",
                    "img/payments/gateways/visa-credit-card.png",
                    "img/payments/gateways/american-express.png",
                    "img/payments/gateways/payment.png"
                },
                Enabled = true,
                Country = "Global",
                ExchangeRate = 0.0567m
            },
            new()
            {
                PaymentMethodId = 4,
                CountryIcon= "img/payments/flags/malaysia.png",
                Provider = "CIMB Clicks",
                DisplayTitle = "CIMB Clicks",
                Currency = "MYR",
                MinAmount = 20.00m,
                MaxAmount = 880.00m,
                Amounts = new List<long>
                {
                    20,
                    50,
                    100,
                    200,
                    500,
                    880
                },
                Icon = new List<string>() { "img/payments/gateways/cimb.png" },
                Enabled = true,
                Country = "Malaysia",
                ExchangeRate = 0.0567m
            },
            new()
            {
                PaymentMethodId = 9,
                CountryIcon = "img/payments/flags/malaysia.png",
                Provider = "BillPlz",
                DisplayTitle = "FPX-MEPS",
                Currency = "MYR",
                MinAmount = 20.00m,
                MaxAmount = 500.00m,
                Amounts = new List<long>
                {
                    20,
                    50,
                    100,
                    200,
                    500
                },
                Icon = new List<string>()
                {
                    "img/payments/gateways/FPX.png",
                    "img/payments/gateways/MEPS.png"
                },
                Enabled = true,
                Country = "Malaysia",
                ExchangeRate = 0.0567m
            },
            new()
            {
                PaymentMethodId = 7,
                CountryIcon = "img/payments/flags/germany.png",
                Provider = "Giropay",
                DisplayTitle = "Giropay",
                Currency = "EUR",
                MinAmount = 15.00m,
                MaxAmount = 950.00m,
                Amounts = new List<long>
                {
                    15,
                    50,
                    100,
                    200,
                    500,
                    950
                },
                Icon = new List<string>() { "img/payments/gateways/Giropay.png" },
                Enabled = true,
                Country = "Germany",
                ExchangeRate = 0.0567m
            },
            new()
            {
                PaymentMethodId = 8,
                CountryIcon = "img/payments/flags/netherlands.png",
                Provider = "iDeal",
                DisplayTitle = "iDeal",
                Currency = "EUR",
                MinAmount = 15.00m,
                MaxAmount = 990.00m,
                Amounts = new List<long>
                {
                    15,
                    50,
                    100,
                    200,
                    500,
                    990
                },
                Icon = new List<string>() { "img/payments/gateways/iDeal.png" },
                Enabled = true,
                Country = "Netherlands, The",
                ExchangeRate = 0.0567m
            },
            new()
            {
                PaymentMethodId = 10,
                CountryIcon = "img/payments/flags/iran.png",
                Provider = "ZarinPal",
                DisplayTitle = "Shetab",
                Currency = "IRR",
                MinAmount = 1_000_000m,
                MaxAmount = 100_000_000m,
                Amounts = new List<long>
                {
                    1_000_000,
                    2_000_000,
                    5_000_000,
                    10_000_000,
                    20_000_000,
                    50_000_000,
                    100_000_000
                },
                Icon = new List<string>() { "img/payments/gateways/Shaparak.png" },
                Enabled = true,
                Country = "Iran",
                ExchangeRate = 1.00m
            },
            new()
            {
                PaymentMethodId = 1,
                CountryIcon = "img/payments/flags/paypal.png",
                Provider = "PayPal",
                DisplayTitle = "PayPal",
                Currency = "USD",
                MinAmount = 50.00m,
                MaxAmount = 500.00m,
                Amounts = new List<long> { 50, 100, 200, 500 },
                Icon = new List<string>(),
                Enabled = false,
                Country = "Global",
                ExchangeRate = 0.0567m
            }
        });
    }
}