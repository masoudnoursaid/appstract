using ClientSdk.PaymentApi.V1;

namespace Payment.Api.Integration.Storage;

public static class TestCaseStorage
{
    public static IEnumerable<TestCaseData> CreatePaymentCases(string currencyShortname)
    {
        yield return new TestCaseData(new CreatePaymentRequest
        {
            Payload = new CreatePaymentPayload
            {
                Items = new List<PaymentItem>
                {
                    new()
                    {
                        Currency = currencyShortname,
                        Description = "This is just a mock item - 1",
                        Name = "Item number 1",
                        Price = "1500",
                        Quantity = 1,
                        Sku = $"ITEM-1_{Guid.NewGuid()}"
                    },
                    new()
                    {
                        Currency = currencyShortname,
                        Description = "This is just a mock item - 2",
                        Name = "Item number 2",
                        Price = "500",
                        Quantity = 1,
                        Sku = $"ITEM-2_{Guid.NewGuid()}"
                    }
                },
                Amount = 2000,
                Currency = currencyShortname,
                Description = "Description from paypal integration test",
                Email = $"customer.email.{Guid.NewGuid()}@email.com",
                Name = $"customer.name.{Guid.NewGuid()}",
                Mobile = "+447404788642",
                InvoiceNumber = $"INVOICE-NUMBER_{Guid.NewGuid()}",
                ClientRedirectUrl = "https://example.com/redirect",
                ClientWebHookUrl = "https://example.com/hook"
            }
        });
    }
}