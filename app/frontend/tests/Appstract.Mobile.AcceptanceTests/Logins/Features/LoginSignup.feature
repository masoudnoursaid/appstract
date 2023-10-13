@Login
Feature:
User login with email account,
and if it is the first time login,
must verify the mobile number with otp

    Background:
        Given system error codes are following
          | Code       | Description                      |
          | 13_592_103 | UnableToSendEmail                |
          | 13_592_108 | RateLimitExceeded                |
          | 13_592_109 | BypassedMinimumIntervalRateLimit |
          | 13_593_103 | InvalidOtp                       |
          | 13_593_104 | OtpHasExpired                    |
          | 13_593_108 | MaximumFailedAttemptsExceeded    |
          | 13_593_111 | OtpNotFound                      |
          | 13_591_101 | InvalidToken                     |
          | 13_591_102 | InvalidIssuer                    |
          | 13_591_103 | InvalidEmail                     |
          | 13_591_104 | ProviderNotFound                 |
          | 13_591_105 | TokenValidationFailed            |
          | 13_591_106 | GetJwksFailed                    |
          | 13_596_103 | InvalidPhoneNumber               |
          | 13_596_104 | BypassedMinimumIntervalRateLimit |
          | 13_596_105 | RateLimitExceeded                |
          | 13_596_107 | RateLimitExceeded                |
          | 13_596_108 | VonageFailed                     |
          | 13_596_110 | SendSmsFailed                    |
          | 13_596_111 | SsoUserNotFound                  |
          | 13_595_101 | OtpNotFound                      |
          | 13_595_102 | MaximumFailedAttemptsExceeded    |
          | 13_595_103 | InvalidOtp                       |
          | 13_595_107 | OtpHasExpired                    |
          | 13_595_110 | SsoUserNotFound                  |
          | 13_595_111 | CanNotUnlinkExistedPhone         |
          | 13_997_104 | InvalidTokenGenerated            |
          | 13_997_107 | AccountNotFound                  |
          | 13_590_103 | QrCodeNotFound                   |
          | 13_590_104 | QrCodeExpired                    |
          | 13_590_105 | QrCodeAlreadyUsed                |
          | 13_590_108 | QrCodeAlreadyConnected           |
          | 13_590_110 | DeviceIsInvalid                  |
        And otp expire time is "300" seconds
        And anonymous device can only request for "10" times for day
        And email Otp generate always return "123456" as value in mocked scenario
        And sms Otp generate always return "123456" as value in mocked scenario
        And user can attempt "5" times with invalid value per generated OTP

    # Mobile client is loading (register device)

    @ignore
    Scenario: User login or signup
        When mobile client request for access token and refresh token with below data
          | Key                | Value               |
          | platformDeviceId   | 6879b8c3d502a7d9    |
          | manufacturer       | Google              |
          | model              | sdk_gphone64_x86_64 |
          | osVersion          | 12                  |
          | isTablet           | false               |
          | isEmulator         | true                |
          | buildNumber        | 13                  |
          | os                 | Android             |
          | displayHeight      | 683                 |
          | displayWidth       | 411                 |
          | displayOrientation | PORTRAIT            |
          | appVersion         | 1.1.293             |
        Then mobile client receive access token and refresh token
          | Key          | Value          |
          | success      | true           |
          | accessToken  | <AccessToken>  |
          | refreshToken | <RefreshToken> |

    # user register with email

        When user submits email with below data
          | Key   | Value        |
          | email | john@doe.com |
        Then server responds with below parameters for submit email
          | Key     | Value |
          | success | true  |

        When user send "9" request email otp with below data
          | Key   | value       |
          | email | tom@doe.com |
        Then server responds with below data for request email otp
          | Key           | Value |
          | requestsCount | 9     |
          | count         | 10    |
        And visible warning box final attempt to request OTP for day.

        When user send last request email otp with below data
          | Key   | value         |
          | email | smith@doe.com |
        Then server responds with below data for request email otp
          | Key           | Value    |
          | requestsCount | 10       |
          | count         | 10       |
          | code          | 13592108 |
        And visible warning box reached maximum allowed OTP per day.

        When user send request invalid email otp with below data
          | Key   | Value         |
          | email | david@doe.com |
          | code  | 654321        |
        Then server responds with below parameter for verify email
          | Key     | Value |
          | success | false |
        And the result should be error code "13593103"

    #user verify email otp

        Given user has registered with email below data
          | Key   | Value         |
          | email | david@doe.com |
        When user send code to verify email with below data
          | Key  | Value  |
          | code | 123456 |
        Then server responds with below parameter for verify email
          | Key          | Value |
          | success      | true  |
          | newUser      | true  |
          | accessToken  |       |
          | refreshToken |       |
        And user should be navigate to "SignupPhoneNumber"

    # user first login add phone number

        When user submits phone number with below data
          | Key         | Value       |
          | phoneNumber | 60121234567 |
          | countryCode | 60          |
          | countryName | Malaysia    |
        Then server responds with below parameters for submit phone number
          | Key     | Value |
          | success | true  |
        And user should be navigate to "PhoneOtp"

        When user send invalid code to verify phone with below data
          | Key   | Value        |
          | phone | +60121234567 |
          | code  | 654321       |
        Then server responds with below parameter for verify phone otp
          | Key     | Value |
          | success | false |
        And the result should be error code "13593103"

        When user send "5" request phone otp with below data
          | Key         | Value       |
          | phoneNumber | 60121234567 |
          | countryCode | 60          |
          | countryName | Malaysia    |
        Then server responds with below data for request phone otp
          | Key           | Value    |
          | requestsCount | 5        |
          | count         | 5        |
          | code          | 13596105 |
        And visible warning box reached maximum allowed OTP per day.

        When user submits OTP with below data
          | Key   | Value        |
          | phone | +60121234567 |
          | code  | 123456       |
        Then server responds with below parameters for verify phone otp
          | Key          | Value          |
          | success      | true           |
          | accessToken  | <AccessToken>  |
          | refreshToken | <RefreshToken> |
        And user should be navigate to "HomePage"

    @ignore
    Scenario: email was already registered navigate to home page
        Given user email was already registered with below data
          | Key   | Value        |
          | email | alex@doe.com |
        When user send code to verify email with below data
          | Key  | Value  |
          | code | 123456 |
        Then server responds with below parameters for verify email
          | Key          | Value |
          | success      | true  |
          | newUser      | false |
          | accessToken  |       |
          | refreshToken |       |
        And user should be navigate to "HomePage"

    # Refresh Token
    @ignore
    Scenario: Renew access token
        When User token expired and send request with below data
          | Key          | Value          |
          | accessToken  | <AccessToken>  |
          | refreshToken | <RefreshToken> |
        Then server responds with below data
          | Key          | Value          |
          | success      | true           |
          | accessToken  | <AccessToken>  |
          | refreshToken | <RefreshToken> |
