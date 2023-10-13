using ClientSdk.AdminApi.V1;

namespace Admin.Api.Integration.Controllers.PaymentMethod.Storage;

public static class PaymentMethodTestCaseStorage
{
    public static IEnumerable<TestCaseData> PaymentMethodTestCases(string title, SourceImplementedGetWay getWay)
    {
        return new List<TestCaseData>
        {
            new(new PaymentMethodModelDto
            {
                Active = true,
                Title = title,
                Icon = "icon.png",
                Provider = "PayPal-Provider",
                Sandbox = true,
                CountryIcon = "NL.png",
                DisplayOrder = 1,
                DisplayTitle = "Paypal",
                GeographicRestriction = "IRAN,IRAQ",
                GeographicSanctions = new List<GeoLocation>
                {
                    new()
                    {
                        CountryName = "IRAN",
                        CountryCode = "123"
                    }
                },
                MaxValue = 100,
                MinValue = 10,
                SupportedCountries = new List<GeoLocation>
                {
                    new()
                    {
                        CountryName = "IRAN",
                        CountryCode = "123"
                    }
                },
                GeographicRestrictionEnforced = true,
                PaymentAmountsLive = "1000",
                PaymentAmountsSandbox = "1001",
                GetWay = getWay
            })
        };
    }
}