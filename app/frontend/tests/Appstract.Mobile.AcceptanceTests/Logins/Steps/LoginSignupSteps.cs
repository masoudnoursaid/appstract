using System;
using TechTalk.SpecFlow;

namespace Appstract.Mobile.AcceptanceTests.Logins.Steps;

[Binding]
public class LoginSignupSteps
{
    [Given(@"system error codes are following")]
    public void GivenSystemErrorCodesAreFollowing(Table table)
    {
        ScenarioContext.StepIsPending();
    }

    [Given(@"otp expire time is ""(.*)"" seconds")]
    public void GivenOtpExpireTimeIsSeconds(string p0)
    {
        ScenarioContext.StepIsPending();
    }

    [Given(@"anonymous device can only request for ""(.*)"" times for day")]
    public void GivenAnonymousDeviceCanOnlyRequestForTimesForDay(string p0)
    {
        ScenarioContext.StepIsPending();
    }

    [Given(@"email Otp generate always return ""(.*)"" as value in mocked scenario")]
    public void GivenEmailOtpGenerateAlwaysReturnAsValueInMockedScenario(string p0)
    {
        ScenarioContext.StepIsPending();
    }

    [Given(@"sms Otp generate always return ""(.*)"" as value in mocked scenario")]
    public void GivenSmsOtpGenerateAlwaysReturnAsValueInMockedScenario(string p0)
    {
        ScenarioContext.StepIsPending();
    }

    [Given(@"user can attempt ""(.*)"" times with invalid value per generated OTP")]
    public void GivenUserCanAttemptTimesWithInvalidValuePerGeneratedOtp(string p0)
    {
        ScenarioContext.StepIsPending();
    }

    [When(@"mobile client request for access token and refresh token with below data")]
    public void WhenMobileClientRequestForAccessTokenAndRefreshTokenWithBelowData(Table table)
    {
        ScenarioContext.StepIsPending();
    }

    [Then(@"mobile client receive access token and refresh token")]
    public void ThenMobileClientReceiveAccessTokenAndRefreshToken(Table table)
    {
        ScenarioContext.StepIsPending();
    }

    [When(@"user submits email with below data")]
    public void WhenUserSubmitsEmailWithBelowData(Table table)
    {
        ScenarioContext.StepIsPending();
    }

    [Then(@"server responds with below parameters for submit email")]
    public void ThenServerRespondsWithBelowParametersForSubmitEmail(Table table)
    {
        ScenarioContext.StepIsPending();
    }

    [When(@"user send ""(.*)"" request email otp with below data")]
    public void WhenUserSendRequestEmailOtpWithBelowData(string p0, Table table)
    {
        ScenarioContext.StepIsPending();
    }

    [Then(@"server responds with below data for request email otp")]
    public void ThenServerRespondsWithBelowDataForRequestEmailOtp(Table table)
    {
        ScenarioContext.StepIsPending();
    }

    [Then(@"visible warning box final attempt to request OTP for day\.")]
    public void ThenVisibleWarningBoxFinalAttemptToRequestOtpForDay()
    {
        ScenarioContext.StepIsPending();
    }

    [When(@"user send last request email otp with below data")]
    public void WhenUserSendLastRequestEmailOtpWithBelowData(Table table)
    {
        ScenarioContext.StepIsPending();
    }

    [Then(@"visible warning box reached maximum allowed OTP per day\.")]
    public void ThenVisibleWarningBoxReachedMaximumAllowedOtpPerDay()
    {
        ScenarioContext.StepIsPending();
    }

    [When(@"user send request invalid email otp with below data")]
    public void WhenUserSendRequestInvalidEmailOtpWithBelowData(Table table)
    {
        ScenarioContext.StepIsPending();
    }

    [Then(@"server responds with below parameter for verify email")]
    public void ThenServerRespondsWithBelowParameterForVerifyEmail(Table table)
    {
        ScenarioContext.StepIsPending();
    }

    [Then(@"the result should be error code ""(.*)""")]
    public void ThenTheResultShouldBeErrorCode(string p0)
    {
        ScenarioContext.StepIsPending();
    }

    [Given(@"user has registered with email below data")]
    public void GivenUserHasRegisteredWithEmailBelowData(Table table)
    {
        ScenarioContext.StepIsPending();
    }

    [When(@"user send code to verify email with below data")]
    public void WhenUserSendCodeToVerifyEmailWithBelowData(Table table)
    {
        ScenarioContext.StepIsPending();
    }

    [Then(@"user should be navigate to ""(.*)""")]
    public void ThenUserShouldBeNavigateTo(string signupPhoneNumber)
    {
        ScenarioContext.StepIsPending();
    }

    [When(@"user submits phone number with below data")]
    public void WhenUserSubmitsPhoneNumberWithBelowData(Table table)
    {
        ScenarioContext.StepIsPending();
    }

    [Then(@"server responds with below parameters for submit phone number")]
    public void ThenServerRespondsWithBelowParametersForSubmitPhoneNumber(Table table)
    {
        ScenarioContext.StepIsPending();
    }

    [When(@"user send invalid code to verify phone with below data")]
    public void WhenUserSendInvalidCodeToVerifyPhoneWithBelowData(Table table)
    {
        ScenarioContext.StepIsPending();
    }

    [Then(@"server responds with below parameter for verify phone otp")]
    public void ThenServerRespondsWithBelowParameterForVerifyPhoneOtp(Table table)
    {
        ScenarioContext.StepIsPending();
    }

    [When(@"user send ""(.*)"" request phone otp with below data")]
    public void WhenUserSendRequestPhoneOtpWithBelowData(string p0, Table table)
    {
        ScenarioContext.StepIsPending();
    }

    [Then(@"server responds with below data for request phone otp")]
    public void ThenServerRespondsWithBelowDataForRequestPhoneOtp(Table table)
    {
        ScenarioContext.StepIsPending();
    }

    [When(@"user submits OTP with below data")]
    public void WhenUserSubmitsOtpWithBelowData(Table table)
    {
        ScenarioContext.StepIsPending();
    }

    [Then(@"server responds with below parameters for verify phone otp")]
    public void ThenServerRespondsWithBelowParametersForVerifyPhoneOtp(Table table)
    {
        ScenarioContext.StepIsPending();
    }

    [When(@"User token expired and send request with below data")]
    public void WhenUserTokenExpiredAndSendRequestWithBelowData(Table table)
    {
        ScenarioContext.StepIsPending();
    }

    [Then(@"server responds with below data")]
    public void ThenServerRespondsWithBelowData(Table table)
    {
        ScenarioContext.StepIsPending();
    }

    [When(@"user email was already registered with below data")]
    public void WhenUserEmailWasAlreadyRegisteredWithBelowData(Table table)
    {
        ScenarioContext.StepIsPending();
    }

    [Then(@"server responds with below parameters for verify email")]
    public void ThenServerRespondsWithBelowParametersForVerifyEmail(Table table)
    {
        ScenarioContext.StepIsPending();
    }

    [Given(@"user email was already registered with below data")]
    public void GivenUserEmailWasAlreadyRegisteredWithBelowData(Table table)
    {
        ScenarioContext.StepIsPending();
    }
}