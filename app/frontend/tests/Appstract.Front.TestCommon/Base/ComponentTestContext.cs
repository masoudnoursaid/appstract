using Appstract.Front.Application.Common.Constants;
using Appstract.TestCommon.Extensions;
using Appstract.TestCommon.FakeServices;
using Blazored.LocalStorage;
using Bunit;
using Moq;

namespace Appstract.TestCommon.Base;

public abstract class ComponentTestContext : TestContext
{
    private readonly Mock<ISyncLocalStorageService> _mockLocalStorage = new();
    public readonly FakeDateTimeService DateTimeService = new();

    protected ComponentTestContext()
    {
        this.RegisterDependencies(_mockLocalStorage.Object, DateTimeService);
    }

    public void SetBaseInfoInLocalStorage(string baseCurrency = "MYR", string userCulture = "en-MY",
        string userTimeZone = "")
    {
        _mockLocalStorage.Setup(u => u.GetItemAsString(UltraToneStorage.BASE_CURRENCY))
            .Returns(baseCurrency);
        _mockLocalStorage.Setup(u => u.GetItemAsString(UltraToneStorage.USER_CULTURE))
            .Returns(userCulture);
        _mockLocalStorage.Setup(u => u.GetItemAsString(UltraToneStorage.USER_TIME_ZONE))
            .Returns(userTimeZone);
    }
}