using Appstract.Front.Mobile.Views.Onboarding.Login.ViewModels;
using Appstract.Mobile.Application.Common.Models;
using Appstract.Mobile.Application.Dto.Authentication;
using Appstract.Mobile.Application.Services;
using FluentAssertions;
using Mopups.Interfaces;
using Moq;

namespace Appstract.Front.Mobile.UnitTests.Logins
{
    public class LoginPhoneViewModelTests
    {
        private Mock<IAuthService> _authServiceMock;
        private LoginPhoneViewModel _loginPhoneViewModel;
        private Mock<INavigationService> _navigationServiceMock;
        private Mock<IPopupNavigation> _popupNavigationMock;

        public LoginPhoneViewModelTests()
        {
        }

        [Fact]
        public void Should_failed_when_invalid_phone()
        {
            ApiResult<PhoneOtpDto> phoneOtp = new() { Success = false };
            _authServiceMock = new Mock<IAuthService>();
            _navigationServiceMock = new Mock<INavigationService>();
            _popupNavigationMock = new Mock<IPopupNavigation>();
            _authServiceMock.Setup(x => x.PhoneOtpAsync("+1201123456789")).ReturnsAsync(phoneOtp);
            _loginPhoneViewModel = new LoginPhoneViewModel(_authServiceMock.Object, _popupNavigationMock.Object,
                _navigationServiceMock.Object);
            _loginPhoneViewModel.Phone = "123456789";

            _loginPhoneViewModel.NextCommand.Execute(null);

            _loginPhoneViewModel.IsEnableNext.Should().BeTrue();
        }
    }
}