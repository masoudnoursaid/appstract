using Appstract.Front.Application.Common.Utilities;
using Appstract.TestCommon.Base;
using FluentAssertions;

namespace UltraTone.UnitTest.Utilities;

public class PhoneUtilTest : ComponentTestContext
{
    [Theory]
    [InlineData("+980000000000", "IR")]
    [InlineData("+310000000000", "NL")]
    [InlineData("+600000000000", "MY")]
    [InlineData("+14845691206","US")]
    [InlineData("-----", "")]
    [InlineData(null, "")]
    public void GetCountryCode(string phoneNumber, string countryCode)
    {
        PhoneUtil.GetCountryCode(phoneNumber).Should().Be(countryCode);
    }
    
    [Theory]
    [InlineData("+98-(000) 000-0000", "+980000000000")]
    [InlineData("", "")]
    [InlineData(null, null)]
    public void NormalizePhoneNumber(string phoneNumber, string expected)
    {
        phoneNumber.NormalizePhoneNumber().Should().Be(expected);
    }
}