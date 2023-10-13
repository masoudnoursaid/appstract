using System;
using System.Collections.Generic;
using System.Linq;
using AngleSharp.Dom;
using Appstract.TestCommon.Base;
using Appstract.TestCommon.Viewers;
using FluentAssertions;
using MudBlazor;

namespace UltraTone.UnitTest.Components.TableFilters;

public class DateRangeTableFilterTest : ComponentTestContext
{
    private bool _tableReloadCalled;
    private DateRange? _selectedDates;
    private bool _datesChangedCalled;

    private void OnDatesChanged(DateRange? dateRange)
    {
        _datesChangedCalled = true;
        _selectedDates = dateRange;
    }

    [Fact]
    public void DateRangeTableFilter_SelectRange_ReturnCorrectDateRangeAndReloadTable()
    {
        DateRange expectedDates = new(new DateTime(2021, 1, 1), new DateTime(2022, 2, 20));
        IRenderedComponent<DateRangeTableFilterViewer> cut = RenderComponent<DateRangeTableFilterViewer>(p => p
            .Add(a => a.Class, "date-range")
            .Add(a => a.Dates, _selectedDates)
            .Add(a => a.DatesChanged, OnDatesChanged)
            .Add(a => a.ReloadTable, () => _tableReloadCalled = true)
        );

        cut.Find(".date-range").Click();

        cut.Find(".mud-picker-calendar-header-transition").Click(); //this open months selector
        cut.Find(".mud-picker-calendar-header-transition").Click(); //this open years selector
        cut.FindAll(".mud-picker-year-container div")
            .First(d => d.Text() == expectedDates.Start!.Value.Year.ToString())
            .Click(); //select year
        cut.Find($".mud-picker-month-container button[aria-label='{expectedDates.Start!.Value:MMMM}']")
            .Click(); //select month
        cut.FindAll(".mud-picker-calendar button p")
            .First(p => p.Text() == expectedDates.Start!.Value.Day.ToString())
            .Click(); //select day

        cut.Find(".mud-picker-calendar-header-transition").Click(); //this open months selector
        cut.Find(".mud-picker-calendar-header-transition").Click(); //this open years selector
        cut.FindAll(".mud-picker-year-container div")
            .First(d => d.Text() == expectedDates.End!.Value.Year.ToString())
            .Click(); //select year
        cut.Find($".mud-picker-month-container button[aria-label='{expectedDates.End!.Value:MMMM}']")
            .Click(); //select month
        cut.FindAll(".mud-picker-calendar button p")
            .First(p => p.Text() == expectedDates.End!.Value.Day.ToString())
            .Click(); //select day

        List<string?> selectedDates= cut.FindAll(".table-filter__date-range-picker--header-input input")
            .Select(e=>e.GetAttribute("value")).ToList();
        selectedDates[0].Should().Be(expectedDates.Start!.Value.ToString("dd-MMM-yyyy"));
        selectedDates[1].Should().Be(expectedDates.End!.Value.ToString("dd-MMM-yyyy"));
        
        cut.Find(".table-filter__ok-btn").Click();
        
        _datesChangedCalled.Should().BeTrue();
        _selectedDates.Should().BeEquivalentTo(expectedDates);
        _tableReloadCalled.Should().BeTrue();
        cut.FindAll(".table-filter__date-range-picker").Any().Should().BeFalse();
    }

    [Fact]
    public void DateRangeTableFilter_ClearButton_MakeDateRangeNullAndReloadTable()
    {
        DateRange dates = new(new DateTime(2021, 1, 1), new DateTime(2022, 2, 20));
        IRenderedComponent<DateRangeTableFilterViewer> cut = RenderComponent<DateRangeTableFilterViewer>(p => p
            .Add(a => a.Class, "date-range")
            .Add(a => a.Dates, _selectedDates)
            .Add(a => a.DatesChanged, OnDatesChanged)
            .Add(a => a.ReloadTable, () => _tableReloadCalled = true)
        );

        cut.Find(".date-range").Click();

        cut.Find(".mud-picker-calendar-header-transition").Click(); //this open months selector
        cut.Find(".mud-picker-calendar-header-transition").Click(); //this open years selector
        cut.FindAll(".mud-picker-year-container div")
            .First(d => d.Text() == dates.Start!.Value.Year.ToString())
            .Click(); //select year
        cut.Find($".mud-picker-month-container button[aria-label='{dates.Start!.Value:MMMM}']")
            .Click(); //select month
        cut.FindAll(".mud-picker-calendar button p")
            .First(p => p.Text() == dates.Start!.Value.Day.ToString())
            .Click(); //select day

        cut.Find(".mud-picker-calendar-header-transition").Click(); //this open months selector
        cut.Find(".mud-picker-calendar-header-transition").Click(); //this open years selector
        cut.FindAll(".mud-picker-year-container div")
            .First(d => d.Text() == dates.End!.Value.Year.ToString())
            .Click(); //select year
        cut.Find($".mud-picker-month-container button[aria-label='{dates.End!.Value:MMMM}']")
            .Click(); //select month
        cut.FindAll(".mud-picker-calendar button p")
            .First(p => p.Text() == dates.End!.Value.Day.ToString())
            .Click(); //select day

        List<string?> selectedDates= cut.FindAll(".table-filter__date-range-picker--header-input input")
            .Select(e=>e.GetAttribute("value")).ToList();
        selectedDates[0].Should().Be(dates.Start!.Value.ToString("dd-MMM-yyyy"));
        selectedDates[1].Should().Be(dates.End!.Value.ToString("dd-MMM-yyyy"));
        
        _datesChangedCalled = false;
        _tableReloadCalled = false;
        cut.Find(".table-filter__clear-btn").Click();

        _datesChangedCalled.Should().BeTrue();
        _selectedDates.Should().BeNull();
        _tableReloadCalled.Should().BeTrue();
        cut.FindAll(".table-filter__date-range-picker").Any().Should().BeFalse();
    }
    
     [Fact]
    public void DateRangeTableFilter_OkButton_ReturnSelectedDateRangeAndReloadTable()
    {
        DateRange expectedDates = new(new DateTime(2021, 1, 1), new DateTime(2022, 2, 20));
        IRenderedComponent<DateRangeTableFilterViewer> cut = RenderComponent<DateRangeTableFilterViewer>(p => p
            .Add(a => a.Class, "date-range")
            .Add(a => a.Dates, _selectedDates)
            .Add(a => a.DatesChanged, OnDatesChanged)
            .Add(a => a.ReloadTable, () => _tableReloadCalled = true)
        );

        cut.Find(".date-range").Click();

        cut.Find(".mud-picker-calendar-header-transition").Click(); //this open months selector
        cut.Find(".mud-picker-calendar-header-transition").Click(); //this open years selector
        cut.FindAll(".mud-picker-year-container div")
            .First(d => d.Text() == expectedDates.Start!.Value.Year.ToString())
            .Click(); //select year
        cut.Find($".mud-picker-month-container button[aria-label='{expectedDates.Start!.Value:MMMM}']")
            .Click(); //select month
        cut.FindAll(".mud-picker-calendar button p")
            .First(p => p.Text() == expectedDates.Start!.Value.Day.ToString())
            .Click(); //select day

        cut.Find(".mud-picker-calendar-header-transition").Click(); //this open months selector
        cut.Find(".mud-picker-calendar-header-transition").Click(); //this open years selector
        cut.FindAll(".mud-picker-year-container div")
            .First(d => d.Text() == expectedDates.End!.Value.Year.ToString())
            .Click(); //select year
        cut.Find($".mud-picker-month-container button[aria-label='{expectedDates.End!.Value:MMMM}']")
            .Click(); //select month
        cut.FindAll(".mud-picker-calendar button p")
            .First(p => p.Text() == expectedDates.End!.Value.Day.ToString())
            .Click(); //select day

        List<string?> selectedDates= cut.FindAll(".table-filter__date-range-picker--header-input input")
            .Select(e=>e.GetAttribute("value")).ToList();
        selectedDates[0].Should().Be(expectedDates.Start!.Value.ToString("dd-MMM-yyyy"));
        selectedDates[1].Should().Be(expectedDates.End!.Value.ToString("dd-MMM-yyyy"));
        
        _datesChangedCalled = false;
        _tableReloadCalled = false;
        cut.Find(".table-filter__ok-btn").Click();

        _datesChangedCalled.Should().BeTrue();
        _selectedDates.Should().BeEquivalentTo(expectedDates);
        _tableReloadCalled.Should().BeTrue();
        cut.FindAll(".table-filter__date-range-picker").Any().Should().BeFalse();
    }
    
       [Fact]
    public void DateRangeTableFilter_CancelButton_ClosePopoverAndNotReloadTable()
    {
        DateRange dates = new(new DateTime(2021, 1, 1), new DateTime(2022, 2, 20));
        IRenderedComponent<DateRangeTableFilterViewer> cut = RenderComponent<DateRangeTableFilterViewer>(p => p
            .Add(a => a.Class, "date-range")
            .Add(a => a.Dates, _selectedDates)
            .Add(a => a.DatesChanged, OnDatesChanged)
            .Add(a => a.ReloadTable, () => _tableReloadCalled = true)
        );

        cut.Find(".date-range").Click();

        cut.Find(".mud-picker-calendar-header-transition").Click(); //this open months selector
        cut.Find(".mud-picker-calendar-header-transition").Click(); //this open years selector
        cut.FindAll(".mud-picker-year-container div")
            .First(d => d.Text() == dates.Start!.Value.Year.ToString())
            .Click(); //select year
        cut.Find($".mud-picker-month-container button[aria-label='{dates.Start!.Value:MMMM}']")
            .Click(); //select month
        cut.FindAll(".mud-picker-calendar button p")
            .First(p => p.Text() == dates.Start!.Value.Day.ToString())
            .Click(); //select day

        cut.Find(".mud-picker-calendar-header-transition").Click(); //this open months selector
        cut.Find(".mud-picker-calendar-header-transition").Click(); //this open years selector
        cut.FindAll(".mud-picker-year-container div")
            .First(d => d.Text() == dates.End!.Value.Year.ToString())
            .Click(); //select year
        cut.Find($".mud-picker-month-container button[aria-label='{dates.End!.Value:MMMM}']")
            .Click(); //select month
        cut.FindAll(".mud-picker-calendar button p")
            .First(p => p.Text() == dates.End!.Value.Day.ToString())
            .Click(); //select day

        List<string?> selectedDates= cut.FindAll(".table-filter__date-range-picker--header-input input")
            .Select(e=>e.GetAttribute("value")).ToList();
        selectedDates[0].Should().Be(dates.Start!.Value.ToString("dd-MMM-yyyy"));
        selectedDates[1].Should().Be(dates.End!.Value.ToString("dd-MMM-yyyy"));
        
        _datesChangedCalled = false;
        _tableReloadCalled = false;
        cut.Find(".table-filter__cancel-btn").Click();

        _datesChangedCalled.Should().BeFalse();
        _tableReloadCalled.Should().BeFalse();
        cut.FindAll(".table-filter__date-range-picker").Any().Should().BeFalse();
    }
}