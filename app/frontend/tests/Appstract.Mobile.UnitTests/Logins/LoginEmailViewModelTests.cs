using Appstract.Mobile.Application.Common.Models;
using Appstract.Mobile.Application.Dto.Authentication;
using Appstract.Mobile.Application.Services;
using Mopups.Interfaces;
using Moq;

namespace Appstract.Front.Mobile.UnitTests.Logins;

public class LoginEmailViewModelTests
{
    private readonly Mock<IAuthService> _authServiceMock;
    private readonly LoginEmailViewModel _loginEmailViewModel;
    private readonly Mock<INavigationService> _navigationServiceMock;
    private readonly Mock<IPopupNavigation> _popupNavigationServiceMock;

    public LoginEmailViewModelTests()
    {
        _authServiceMock = new Mock<IAuthService>();
        _navigationServiceMock = new Mock<INavigationService>();
        _popupNavigationServiceMock = new Mock<IPopupNavigation>();
        _loginEmailViewModel = new LoginEmailViewModel(_authServiceMock.Object, _navigationServiceMock.Object);
    }

    [Fact]
    public void Should_failed_when_invalid_email()
    {
        ApiResult<EmailOtpResultDto> emailOtp = new() { Success = false };
        _authServiceMock.Setup(x => x.EmailOtpAsync("john@doe")).ReturnsAsync(emailOtp);
        _loginEmailViewModel.Email = "john@doe";

        _loginEmailViewModel.EmailCompletedCommand.Execute(null);

        _authServiceMock.Verify(x => x.EmailOtpAsync("john@doe"), Times.AtLeastOnce);
        _loginEmailViewModel.IsVisibleErrorBox.Should().BeTrue();
    }

    [Fact]
    public void When_tap_back_button_visible_apple_google_button()
    {
        _loginEmailViewModel.VisibleSegmentCommand.Execute(null);

        _loginEmailViewModel.Email.Should().BeNullOrEmpty();
        _loginEmailViewModel.IsVisibleSegment.Should().BeTrue();
        _loginEmailViewModel.IsVisibleNext.Should().BeFalse();
    }

    [Fact]
    public void When_tap_entry_email_hidden_apple_google_button()
    {
        _loginEmailViewModel.TapEmailCommand.Execute(null);

        _loginEmailViewModel.IsVisibleSegment.Should().BeFalse();
        _loginEmailViewModel.IsVisibleNext.Should().BeTrue();
        _loginEmailViewModel.IsVisibleErrorBox.Should().BeFalse();
    }
}