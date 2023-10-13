using System.Linq;
using AngleSharp.Dom;
using Appstract.Front.Domain.Enums;
using Appstract.TestCommon.Base;
using Appstract.TestCommon.Viewers;
using FluentAssertions;
using Nextended.Core.Helper;

namespace Appstract.UnitTest.Components.TableFilters;

public class PhoneTableFilterTest : ComponentTestContext
{
    private string _phoneNumber = string.Empty;
    private bool _phoneNumberChangedCalled;

    private void PhoneNumberChanged(string phoneNumber)
    {
        _phoneNumber = phoneNumber;
        _phoneNumberChangedCalled = true;
    }

    private FilterMatchType _queryType;
    private bool _queryTypeChangedCalled;

    private void QueryTypeChanged(FilterMatchType queryType)
    {
        _queryType = queryType;
        _queryTypeChangedCalled = true;
    }

    private bool _tableReloadCalled;

    [Fact]
    public void PhoneTableFilter_QueryTypeIsExactAndPhoneNumberEntered_ReturnCorrectValue()
    {
        const string expectedPhoneNumber = "+989123456789";
        IRenderedComponent<PhoneTableFilterViewer> cut = RenderComponent<PhoneTableFilterViewer>(p => p
            .Add(a => a.Title, "Phone")
            .Add(a => a.Class, "phone-filter")
            .Add(a => a.ReloadTable, () => _tableReloadCalled = true)
            .Add(a => a.PhoneNumber, _phoneNumber)
            .Add(a => a.PhoneNumberChanged, PhoneNumberChanged)
            .Add(a => a.PhoneNumberQueryType, _queryType)
            .Add(a => a.PhoneNumberQueryTypeChanged, QueryTypeChanged)
        );

        cut.Find(".phone-filter").Click();

        cut.Find(".table-filter__query-type").Click();
        cut.FindAll(".mud-list-item p").First(p => p.Text() == FilterMatchType.Exact.ToDescriptionString()).Click();

        cut.WaitForElement(".table-filter__phone-number__input input");
        cut.Find(".table-filter__phone-number__input input").Input(expectedPhoneNumber);
        cut.Find(".table-filter__phone-number__input input").KeyUp(Key.Enter);

        cut.Find(".table-filter__ok-btn").Click();

        cut.WaitForAssertion(() =>
        {
            cut.FindAll(".table-filter__popover-title").Any().Should().BeFalse();
        });
        _phoneNumberChangedCalled.Should().BeTrue();
        _phoneNumber.Should().BeEquivalentTo(expectedPhoneNumber);
        _queryTypeChangedCalled.Should().BeTrue();
        _queryType.Should().Be(FilterMatchType.Exact);
        _tableReloadCalled.Should().BeTrue();
    }

    [Fact]
    public void PhoneTableFilter_QueryTypeIsStartWithAndPhoneNumberEntered_ReturnCorrectValue()
    {
        const string expectedPhoneNumber = "+98912345";
        IRenderedComponent<PhoneTableFilterViewer> cut = RenderComponent<PhoneTableFilterViewer>(p => p
            .Add(a => a.Title, "Phone")
            .Add(a => a.Class, "phone-filter")
            .Add(a => a.ReloadTable, () => _tableReloadCalled = true)
            .Add(a => a.PhoneNumber, _phoneNumber)
            .Add(a => a.PhoneNumberChanged, PhoneNumberChanged)
            .Add(a => a.PhoneNumberQueryType, _queryType)
            .Add(a => a.PhoneNumberQueryTypeChanged, QueryTypeChanged)
        );

        cut.Find(".phone-filter").Click();

        cut.Find(".table-filter__query-type").Click();
        cut.FindAll(".mud-list-item p").First(p => p.Text() == FilterMatchType.StartWith.ToDescriptionString())
            .Click();

        cut.WaitForElement("input[name='table-filter__phone-number']");
        cut.Find("input[name='table-filter__phone-number']").Input(expectedPhoneNumber);
        cut.Find("input[name='table-filter__phone-number']").Change(expectedPhoneNumber);

        cut.Find(".table-filter__ok-btn").Click();

        cut.WaitForAssertion(() =>
        {
            cut.FindAll(".table-filter__popover-title").Any().Should().BeFalse();
        });
        _phoneNumberChangedCalled.Should().BeTrue();
        _phoneNumber.Should().BeEquivalentTo(expectedPhoneNumber);
        _queryTypeChangedCalled.Should().BeTrue();
        _queryType.Should().Be(FilterMatchType.StartWith);
        _tableReloadCalled.Should().BeTrue();
    }

    [Fact]
    public void PhoneTableFilter_ClearButton_RemoveValue()
    {
        const string phoneNumber = "+98912345";
        IRenderedComponent<PhoneTableFilterViewer> cut = RenderComponent<PhoneTableFilterViewer>(p => p
            .Add(a => a.Title, "Phone")
            .Add(a => a.Class, "phone-filter")
            .Add(a => a.ReloadTable, () => _tableReloadCalled = true)
            .Add(a => a.PhoneNumber, _phoneNumber)
            .Add(a => a.PhoneNumberChanged, PhoneNumberChanged)
            .Add(a => a.PhoneNumberQueryType, _queryType)
            .Add(a => a.PhoneNumberQueryTypeChanged, QueryTypeChanged)
        );

        cut.Find(".phone-filter").Click();

        cut.Find(".table-filter__query-type").Click();
        cut.FindAll(".mud-list-item p").First(p => p.Text() == FilterMatchType.StartWith.ToDescriptionString())
            .Click();

        cut.WaitForElement("input[name='table-filter__phone-number']");
        cut.Find("input[name='table-filter__phone-number']").Input(phoneNumber);
        cut.Find("input[name='table-filter__phone-number']").Change(phoneNumber);

        cut.Find(".table-filter__ok-btn").Click();
        cut.WaitForAssertion(() =>
        {
            cut.FindAll(".table-filter__popover-title").Any().Should().BeFalse();
        });

        _tableReloadCalled = false;
        _phoneNumberChangedCalled = false;
        _queryTypeChangedCalled = false;
        cut.Find(".phone-filter").Click();
        cut.WaitForAssertion(() =>
        {
            cut.FindAll(".table-filter__clear-btn").Any().Should().BeTrue();
        });
        cut.Find(".table-filter__clear-btn").Click();

        cut.WaitForAssertion(() =>
        {
            cut.FindAll(".table-filter__popover-title").Any().Should().BeFalse();
        });
        _phoneNumberChangedCalled.Should().BeTrue();
        _phoneNumber.Should().BeEmpty();
        _queryTypeChangedCalled.Should().BeTrue();
        _queryType.Should().Be(FilterMatchType.StartWith);
        _tableReloadCalled.Should().BeTrue();
    }

    [Fact]
    public void PhoneTableFilter_CancelButton_ReturnLastValueAndNotReloadTable()
    {
        const string expectedPhoneNumber = "+98912345";
        IRenderedComponent<PhoneTableFilterViewer> cut = RenderComponent<PhoneTableFilterViewer>(p => p
            .Add(a => a.Title, "Phone")
            .Add(a => a.Class, "phone-filter")
            .Add(a => a.ReloadTable, () => _tableReloadCalled = true)
            .Add(a => a.PhoneNumber, _phoneNumber)
            .Add(a => a.PhoneNumberChanged, PhoneNumberChanged)
            .Add(a => a.PhoneNumberQueryType, _queryType)
            .Add(a => a.PhoneNumberQueryTypeChanged, QueryTypeChanged)
        );

        cut.Find(".phone-filter").Click();

        cut.Find(".table-filter__query-type").Click();
        cut.FindAll(".mud-list-item p").First(p => p.Text() == FilterMatchType.StartWith.ToDescriptionString())
            .Click();

        cut.WaitForElement("input[name='table-filter__phone-number']");
        cut.Find("input[name='table-filter__phone-number']").Input(expectedPhoneNumber);
        cut.Find("input[name='table-filter__phone-number']").Change(expectedPhoneNumber);

        cut.Find(".table-filter__ok-btn").Click();
        cut.WaitForAssertion(() =>
        {
            cut.FindAll(".table-filter__popover-title").Any().Should().BeFalse();
        });

        _tableReloadCalled = false;
        _phoneNumberChangedCalled = false;
        _queryTypeChangedCalled = false;
        cut.Find(".phone-filter").Click();
        cut.WaitForAssertion(() =>
        {
            cut.FindAll(".table-filter__cancel-btn").Any().Should().BeTrue();
        });
        cut.Find(".table-filter__cancel-btn").Click();

        cut.WaitForAssertion(() =>
        {
            cut.FindAll(".table-filter__popover-title").Any().Should().BeFalse();
        });
        _tableReloadCalled.Should().BeFalse();
    }
}