using System.Collections.Generic;
using System.Linq;
using AngleSharp.Common;
using AngleSharp.Dom;
using Appstract.AcceptanceTest.Common.Dto;
using Appstract.AcceptanceTest.Common.Dto.CloudSimCard;
using Appstract.AcceptanceTest.Contexts;
using Appstract.AcceptanceTest.Drivers;
using Appstract.Front.Application.Common.Resources;
using Appstract.Front.Domain.Models;
using Appstract.Front.Domain.Models.CloudSimCard;
using Bunit;
using FluentAssertions;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace Appstract.AcceptanceTest.StepDefinitions;

[Binding]
public class PurchaseCloudSimCardStepDefinitions
{
    private readonly PurchaseCloudSimCardDriver _driver;
    private readonly PurchaseCloudSimCardBackgroundContext _context;

    public PurchaseCloudSimCardStepDefinitions(
        PurchaseCloudSimCardDriver driver,
        PurchaseCloudSimCardBackgroundContext context)
    {
        _driver = driver;
        _context = context;
    }

    [Given(@"user culture in Cloud SIM Card page is ""(.*)""")]
    public void GivenUserCultureInCloudSimCardPageIs(string culture)
    {
        _driver.SetBaseInfoInLocalStorage(userCulture: culture);
    }

    [When(@"the purchase Cloud SIM Card component is initialized")]
    public void WhenThePurchaseCloudSimCardComponentIsInitialized()
    {
        _driver.RenderComponent();
    }

    [Then(@"the country and carrier table loading spinner visibility is ""(.*)""")]
    public void ThenTheCountryAndCarrierTableLoadingSpinnerVisibilityIs(bool state)
    {
        _driver.Cut.WaitForAssertion(() =>
        {
            _driver.IsCountryAndCarriersTableLoadingVisible().Should().Be(state);
        });
    }

    [Then(@"the delegate to get country and carriers of Cloud SIM Card is called")]
    public void ThenTheDelegateToGetCountryAndCarriersOfCloudSimCardIsCalled()
    {
        _context.IsDelegateToGetCountryAndCarriersCalled.Should().BeTrue();
    }

    [Then(@"progress steps indicator in purchase Cloud SIM Card show as below")]
    public void ThenProgressStepsIndicatorInPurchaseCloudSimCardShowAsBelow(Table table)
    {
        List<ProgressStepsIndicatorDto> expected = table.CreateSet<ProgressStepsIndicatorDto>().ToList();
        List<ProgressStepsIndicatorDto> actual = _driver.GetProgressStepsIndicator();
        actual.Should().BeEquivalentTo(expected);
    }

    [When(@"the delegate to get available country and carriers of Cloud SIM Card answered with below data")]
    public void WhenTheDelegateToGetAvailableCountryAndCarriersOfCloudSimCardAnsweredWithBelowData(Table table)
    {
        _context.GetCloudSimCardCountryCarriersDelegateResponse =
            table.CreateSet<CloudSimCardCountryCarrierModel>().ToList();
        _driver.ContinueCountryCarriersDelegateExecution();
    }

    [Then(@"user can view below country and carriers of available Cloud SIM Cards")]
    public void ThenUserCanViewBelowCountryAndCarriersOfAvailableCloudSimCards(Table table)
    {
        List<CloudSimCardCountryCarrierDto> expected = table.CreateSet<CloudSimCardCountryCarrierDto>().ToList();
        List<CloudSimCardCountryCarrierDto> actual = _driver.GetCountryCarrierTableResponse();
        actual.Should().BeEquivalentTo(expected);
    }

    [When(@"user check Original Currency checkbox of country and carrier page")]
    public void WhenUserCheckOriginalCurrencyCheckboxOfCountryAndCarrierPage()
    {
        _driver.Cut.Find(".cloud-sim-card__original-currency input").Change(true);
    }

    [When(@"user select the table row with ""(.*)"" as country and ""(.*)"" as carrier")]
    public void WhenUserSelectTheTableRowWithAsCountryAndAsCarrier(string country, string carrier)
    {
        _driver.Cut.FindAll(".cloud-sim-card__country-carrier tbody tr").First(tr =>
            tr.Children.Any(td =>
                td.Attributes["data-label"]!.Value == CloudSimCardResource.Country &&
                td.TextContent.Trim() == country) &&
            tr.Children.Any(td =>
                td.Attributes["data-label"]!.Value == CloudSimCardResource.Carrier && td.TextContent.Trim() == carrier)
        ).Click();
    }

    [Then(@"the Cloud SIM Card mobile numbers table loading spinner visibility is ""(.*)""")]
    public void ThenTheCloudSimCardMobileNumbersTableLoadingSpinnerVisibilityIs(bool state)
    {
        _driver.IsPhoneNumbersTableLoadingVisible().Should().Be(state);
    }

    [Then(@"the Cloud SIM Card mobile numbers table filters is as below")]
    public void ThenTheCloudSimCardMobileNumbersTableFiltersIsAsBelow(Table table)
    {
        string countryFilter = _driver.Cut.FindAll(".cloud-sim-card__country-filter p")[1].Text().Trim();
        string carrierFilter = _driver.Cut.FindAll(".cloud-sim-card__carrier-filter p")[1].Text().Trim();

        string country = table.Rows.First(r => r.Values.GetItemByIndex(0) == "Country").Values.GetItemByIndex(1);
        string carrier = table.Rows.First(r => r.Values.GetItemByIndex(0) == "Carrier").Values.GetItemByIndex(1);

        countryFilter.Should().Be(country);
        carrierFilter.Should().Be(carrier);
    }

    [When(@"the delegate to get available mobile numbers of Cloud SIM Card answered with below data")]
    public void WhenTheDelegateToGetAvailableMobileNumbersOfCloudSimCardAnsweredWithBelowData(Table table)
    {
        _context.GetCloudSimCardMobileNumbersDelegateResponse =
            table.CreateSet<CloudSimCardMobileNumberModel>().ToList();
        _driver.ContinueMobileNumbersDelegateExecution();
    }

    [Then(@"user can view below available mobile numbers of Cloud SIM Card as below")]
    public void ThenUserCanViewBelowAvailableMobileNumbersOfCloudSimCardAsBelow(Table table)
    {
        List<CloudSimCardMobileNumberDto> expected = table.CreateSet<CloudSimCardMobileNumberDto>().ToList();
        List<CloudSimCardMobileNumberDto> actual = _driver.GetPhoneNumbersTableResponse();
        actual.Should().BeEquivalentTo(expected);
    }

    [Then(@"the next button of Cloud SIM Card mobile numbers page enable is ""(.*)""")]
    public void ThenTheNextButtonOfCloudSimCardMobileNumbersPageEnableIs(bool state)
    {
        _driver.Cut.Find(".cloud-sim-card__purchase-phone-input").ClassList
            .Contains("cloud-sim-card__purchase-phone-input--active")
            .Should().Be(state);
    }

    [When(@"user select the Cloud SIM Card mobile numbers table row with ""(.*)"" as mobile number")]
    public void WhenUserSelectTheCloudSimCardMobileNumbersTableRowWithAsMobileNumber(string number)
    {
        _driver.Cut.FindAll(".cloud-sim-card__mobile-numbers tbody tr").First(tr =>
            tr.Children.Any(td =>
                td.HasAttribute("data-label") &&
                td.Attributes["data-label"]!.Value == CloudSimCardResource.MobileNumber &&
                td.TextContent.Trim() == number)
        ).Click();
    }

    [Then(@"below numbers exists in drop down of the next button of Cloud SIM Card mobile numbers page")]
    public void ThenBelowNumbersExistsInDropDownOfTheNextButtonOfCloudSimCardMobileNumbersPage(Table table)
    {
        List<string> expected = table.Rows.Select(r => r["MobileNumber"]).ToList();
        _driver.Cut.Find(".cloud-sim-card__purchase-phone-input-menu button").Click();
        List<string> actual = _driver.Cut.FindAll(".cloud-sim-card__purchase-phone-input-list>div")
            .Select(e => e.Text().Trim()).ToList();

        actual.Should().BeEquivalentTo(expected);
    }

    [When(@"user click the next button of Cloud SIM Card mobile numbers page")]
    public void WhenUserClickTheNextButtonOfCloudSimCardMobileNumbersPage()
    {
        _driver.Cut.Find(".cloud-sim-card__purchase-phone-input .cloud-sim-card__purchase-phone-next").Click();
    }

    [Then(@"delegate to get cloud sim card port price info is called")]
    public void ThenDelegateToGetCloudSimCardPortPriceInfoIsCalled()
    {
        _context.IsDelegateToGetCloudSimCardPortPriceInfoCalled.Should().BeTrue();
    }

    [When(@"delegate to get cloud sim card port price info response as below")]
    public void WhenDelegateToGetCloudSimCardPortPriceInfoResponseAsBelow(Table table)
    {
        _context.GetCloudSimCardPortPriceInfoDelegateResponse = table.CreateInstance<CloudSimCardPortPriceInfoModel>();
        _driver.ContinuePortPriceInfoDelegateExecution();
    }

    [Then(@"user can view price info of selected Cloud SIM Cards as below")]
    public void WhenUserCanViewPriceInfoOfSelectedCloudSimCardsAsBelow(Table table)
    {
        List<CloudSimCardPriceInfoDto> expected = table.CreateSet<CloudSimCardPriceInfoDto>().ToList();
        List<CloudSimCardPriceInfoDto> actual = _driver.GetPriceInfoTableResponse();
        actual.Should().BeEquivalentTo(expected);
    }

    [Then(@"total checkout in cloud sim card port type page is ""(.*)""")]
    public void WhenTotalCheckoutInCloudSimCardPortTypePageIs(string totalCheckout)
    {
        _driver.Cut.Find(".cloud-sim-card__price-total-checkout").Text().Should().Be(totalCheckout);
    }

    [When(@"user click on Payment button in Cloud SIM Card port type page")]
    public void WhenUserClickOnPaymentButtonInCloudSimCardPortTypePage()
    {
        _driver.Cut.Find(".cloud-sim-card__payment-btn").Click();
    }

    [Then(@"the Cloud SIM Card payment page loading spinner visibility is ""(.*)""")]
    public void ThenTheCloudSimCardPaymentPageLoadingSpinnerVisibilityIs(bool isLoading)
    {
        _driver.IsCloudSimCardPaymentPageLoaded().Should().Be(!isLoading);
    }

    [Then(@"delegate to get user wallet info in Cloud SIM Card payment page is called")]
    public void ThenDelegateToGetUserWalletInfoInCloudSimCardPaymentPageIsCalled()
    {
        _context.IsDelegateToGetUserWalletInfoCalled.Should().BeTrue();
    }

    [When(@"delegate to get user wallet info in Cloud SIM Card payment page response as below")]
    public void WhenDelegateToGetUserWalletInfoInCloudSimCardPaymentPageResponseAsBelow(Table table)
    {
        _context.GetUserWalletInfoDelegateResponse = table.CreateInstance<UserWalletInfo>();
        _driver.ContinueGetUserWalletInfoExecution();
    }

    [Then(@"user in Cloud SIM Card payment page can see payment detail info as below")]
    public void ThenUserInCloudSimCardPaymentPageCanSeePaymentDetailInfoAsBelow(Table table)
    {
        string expectedDiscount =
            table.Rows.First(r => r.Values.GetItemByIndex(0) == "Discount").Values.GetItemByIndex(1);
        string expectedTotal = table.Rows.First(r => r.Values.GetItemByIndex(0) == "Total").Values.GetItemByIndex(1);

        string discount = _driver.Cut.Find(".cloud-sim-card__payment-description-discount").Text();
        string total = string.Join(' ', _driver.Cut.FindAll(".cloud-sim-card__payment-description-total p")
            .Select(p => p.Text().Trim()).ToList());

        discount.Should().Be(expectedDiscount);
        total.Should().Be(expectedTotal);
    }
}