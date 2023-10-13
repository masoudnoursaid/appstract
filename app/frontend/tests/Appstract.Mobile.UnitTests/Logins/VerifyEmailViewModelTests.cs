using Appstract.Front.Mobile.Views.Onboarding.Login.ViewModels;
using Appstract.Mobile.Application.Common.Consts;
using Appstract.Mobile.Application.Common.Models;
using Appstract.Mobile.Application.Dto.Authentication;
using Appstract.Mobile.Application.Services;
using FluentAssertions;
using Mopups.Interfaces;
using Moq;

namespace Appstract.Front.UnitTests.Logins
{
    public class VerifyEmailViewModelTests
    {
        private readonly Mock<IAuthService> _authServiceMock;
        private readonly Mock<INavigationService> _navigationServiceMock;
        private readonly Mock<IPopupNavigation> _popupNavigationServiceMock;
        private readonly VerifyEmailViewModel _verifyEmailViewModel;
        private readonly Mock<ISecureStorage> _secureStorageMock;

        public VerifyEmailViewModelTests()
        {
            _authServiceMock = new Mock<IAuthService>();
            _navigationServiceMock = new Mock<INavigationService>();
            _popupNavigationServiceMock = new Mock<IPopupNavigation>();
            _secureStorageMock = new Mock<ISecureStorage>();
            _verifyEmailViewModel = new VerifyEmailViewModel(_authServiceMock.Object, _secureStorageMock.Object,
                _popupNavigationServiceMock.Object, _navigationServiceMock.Object);
        }

        [Fact]
        public void User_should_be_requested_new_otp()
        {
            _verifyEmailViewModel.Email = "john@doe.com";
            _verifyEmailViewModel.IsEnableSubmit = false;
            _verifyEmailViewModel.Duration = new TimeSpan(0, 0, 0, 0, 0);
            ApiResult<EmailOtpResultDto> apiResult =
                new ApiResult<EmailOtpResultDto> { Success = true, Data = new EmailOtpResultDto { RequestsCount = 1 } };
            _authServiceMock.Setup(x => x.EmailOtpAsync(_verifyEmailViewModel.Email)).ReturnsAsync(apiResult);

            _verifyEmailViewModel.NewOtpCommand.Execute(null);

            _verifyEmailViewModel.IsVisibleOtpRequest.Should().BeTrue();
        }

        [Fact]
        public void Verify_email_otp_should_be_successful()
        {
            _verifyEmailViewModel.Code = "123456";
            ApiResult<VerifyEmailOtpDto> result =
                new ApiResult<VerifyEmailOtpDto> { Data = new VerifyEmailOtpDto { NewUser = true }, Success = true };
            _authServiceMock.Setup(x => x.VerifyEmailOtpAsync(_verifyEmailViewModel.Code)).ReturnsAsync(result);
            _secureStorageMock.Setup(x => x.SetAsync(SecureStorageLocal.USER_SING_IN, "false"))
                .Returns(Task.CompletedTask);
            _navigationServiceMock.Setup(x => x.NavigateToAsync(RoutePage.LOGIN_PHONE_PAGE, null))
                .Returns(Task.CompletedTask);

            _verifyEmailViewModel.SubmitCommand.Execute(null);

            _verifyEmailViewModel.IsEnableSubmit.Should().BeFalse();
            _verifyEmailViewModel.IsVisibleErrorBox.Should().BeFalse();
            _verifyEmailViewModel.ErrorCode.Should().BeNullOrEmpty();
        }
    }
}