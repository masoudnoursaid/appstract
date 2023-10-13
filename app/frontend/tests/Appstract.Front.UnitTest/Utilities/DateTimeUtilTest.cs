using System;
using Appstract.Front.Application.Common.Utilities;
using Appstract.TestCommon.Base;
using FluentAssertions;

namespace UltraTone.UnitTest.Utilities;

public class DateTimeUtilTest : ComponentTestContext
{
    [Theory]
    [InlineData("2021-01-02 03:04:05", "", "2021-01-02 03:04:05")]
    [InlineData("2021-01-02 03:04:05", "Asia/Kuala_Lumpur", "2021-01-02 11:04:05")]
    public void DateTimeUtilTest_ToTimeZoneMethodCalled_ReturnDateFitToUserBrowserTimeZone(DateTime dateTime,
        string timeZone, DateTime expected)
    {
        dateTime.ToTimeZone(timeZone).Should().Be(expected);
    }

    [Theory]
    [InlineData(null, "", null)]
    [InlineData(null, "Asia/Kuala_Lumpur", null)]
    public void DateTimeUtilTest_ToTimeZoneMethodWithNullValueCalled_MustReturnNull(DateTime? dateTime, string timeZone,
        DateTime? expected)
    {
        dateTime.ToTimeZone(timeZone).Should().Be(expected);
    }

    [Theory]
    [InlineData("2021-01-02 03:04:05", "", "2021-01-02 03:04:05")]
    [InlineData("2021-01-02 11:04:05", "Asia/Kuala_Lumpur", "2021-01-02 03:04:05")]
    public void DateTimeUtilTest_ToToUtcMethodCalled_ReturnUtcDateFitToUserBrowserTimeZone(DateTime dateTime,
        string timeZone, DateTime expected)
    {
        dateTime.ToUtc(timeZone).Should().Be(expected);
    }

    [Theory]
    [InlineData(null, "", null)]
    [InlineData(null, "Asia/Kuala_Lumpur", null)]
    public void DateTimeUtilTest_ToUtcMethodWithNullValueCalled_MustReturnNull(DateTime? dateTime, string timeZone,
        DateTime? expected)
    {
        dateTime.ToUtc(timeZone).Should().Be(expected);
    }

    [Theory]
    [InlineData("2021-01-02 03:04:05", "", true, "02-Jan-2021 03:04:05 AM")]
    [InlineData("2021-01-02 03:04:05", "Asia/Kuala_Lumpur", true, "02-Jan-2021 11:04:05 AM")]
    [InlineData("2021-01-02 03:04:05", "", false, "02-Jan-2021 03:04 AM")]
    [InlineData("2021-01-02 03:04:05", "Asia/Kuala_Lumpur", false, "02-Jan-2021 11:04 AM")]
    public void DateTimeUtilTest_ToTimeZoneStringMethodCalled_ReturnDateStringFitToUserBrowserTimeZone(
        DateTime dateTime, string timeZone, bool hasSeconds, string expected)
    {
        dateTime.ToTimeZoneString(timeZone, hasSeconds).Should().Be(expected);
    }

    [Theory]
    [InlineData(null, "", true, "")]
    [InlineData(null, "Asia/Kuala_Lumpur", true, "")]
    [InlineData(null, "", false, "")]
    [InlineData(null, "Asia/Kuala_Lumpur", false, "")]
    public void DateTimeUtilTest_ToTimeZoneStringMethodWithNullValueCalled_MustReturnEmptyString(DateTime? dateTime,
        string timeZone, bool hasSeconds, string expected)
    {
        dateTime.ToTimeZoneString(timeZone, hasSeconds).Should().Be(expected);
    }
    
    [Theory]
    [InlineData(0, "now")]
    [InlineData(1, "1 minute ago")]
    [InlineData(5, "5 minutes ago")]
    [InlineData(60, "1 hour ago")]
    [InlineData(1400, "23 hours ago")]
    public void DateTimeUtilTest_ToPrettyStringMethodCalledWithLessThanOneDayDates_MustReturnCorrectString(int minuteBefore,string expected)
    {
        DateTime.Now.AddMinutes(-1 * minuteBefore).ToPrettyString().Should().Be(expected);
    }

    [Fact]
    public void DateTimeUtilTest_ToPrettyStringMethodCalledWithMoreThanOneDayDates_MustReturnCorrectString()
    {
        DateTime dateTime = new(2000, 01, 02, 16, 04, 05);
        dateTime.ToPrettyString().Should().Be("02-Jan-2000");
        dateTime.ToPrettyString(ifIsDateHasTime: true).Should().Be("02-Jan-2000 16:04:05");
    }
}