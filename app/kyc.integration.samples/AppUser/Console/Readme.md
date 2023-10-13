# Kyc.Client.Sdk  [![.NET](https://img.shields.io/badge/--512BD4?logo=.net&logoColor=ffffff)](https://dotnet.microsoft.com/)
![version](https://img.shields.io/badge/version-1.0.0-blue)

# Introduction
The KYC SDK provides a straightforward method to integrate your application with the KYC process,
allowing you to authenticate your users' passports and faces to ensure that your clients
are authorized by approved-recognized issuers, like government.
Visit the [official website]() for more information

# Installation
> nuget install Kyc.Client.Sdk -Source "GitLab"

> nuget source Add -Name "GitLab" -Source "https://gitlab-registry.maxtld.com/api/v4/projects/25/packages/nuget/index.json" -UserName <your_username> -Password <your_token>

# Get Started
```csharp
HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);
builder.Services.ConfigureKycClient(config =>
{
    config.ConnectionConfiguration.Address = "https://kyc-domain.com";
    config.ConnectionConfiguration.Timeout = 10;
    // Support Rsa encryption
    config.SecurityConfiguration.Method = SecurityMethod.RSA;
});
```
> **Warning:** Each session has its own token and public key, so it is necessary to 
> provide both the ``Token`` and ``PublicKey`` for complete authentication.

Initialize your security policy each time you 
receive a ``Session Token`` and an ``RSA Public Key`` from the server.
```csharp
// Get this from your server
var myTestPublicKey = "1U+Lay657nugTf4+t4htSydH5RwJ3EwXGjWEQt6K3k7Z2bVEziGqJEHAKl6u9zSzC6UMt5VNt6f17H0+ZYqOl4nOAGWlb+2iZjTwtuSz4iYZj1ZB5isse25br6rJ/zrbVayyB0wTHOOmWhvr9lPpWQfsm4kZ/GwF8GO0ckHedw4jaxXNguFQpoK5xxeG1MEZ8rNgczzfDHtNrn6w75x1zHR7jGoW32QqNGOhfMgT3vOOjD6/K91l3Gc+Fe+vm31Sp59P6cBSBEXmfzUL4fveL5311qN+Gq3BECw5zy6pQ/9ah0cv9V+2bymUN9zCFDcCDontptcsule4FTIVoG7BLQ==";
// Get this from your server
var myTestToken = "1181c4467c9e8073d3451c24e493935089d17ac92067da1d572ab272b741f4ff";

// Seed your policy each time you want to make new kyc process
securityService.SeedSecurityPolicy(myTestToken, myTestPublicKey);
```

Congratulations! You have successfully configured all the necessary components for the KYC 
process in your client application. Now, you can obtain an ``IKycClient`` instance from 
your ``ServiceProvider`` or inject it into your constructor and begin using it.

# How to interact with ``KycClient``

First you need to upload your client [Nfc Data](),
see [Biometric Passport](https://en.wikipedia.org/wiki/Biometric_passport). 
Here is a example of uploading passport biometric data.
```csharp
 var passportInfo = new CreatePassportRecordRequest()
 {
     IssuingCountryCode = "NL",
     Gender = Gender.Female,
     CertificateThumbprint = "cert-thumb",
     DataGroups = new List<string>() { "group-1", "group-2" },
     DateOfBirth = DateTimeOffset.Now,
     DocumentNumber = "19403920194857",
     ExpiryDate = DateTimeOffset.Now,
     FaceImage = "base64-img",
     GivenName = "Jeff",
     Issuer = "Issuer",
     Lastname = "Varket",
     Nationality = "NL",
     Subject = "Subject",
     IssuedDate = DateTimeOffset.Now,
     LdsVersion = "0.0",
     SerialNumber = "478278",
     SignatureAlgorithm = "Rsa",
     ValidFrom = DateTimeOffset.Now,
     NationalIdNumber = "3847281",
     TypeOfAccessControl = "Any",
     PublicKeyAlgorithm = "rsa",
     DocumentType = DocumentType.Passport
 };
 var resultOfUploadPassportInfo = kycClient.UploadPassportInformation(passportInfo).Result;
```

Next, you should upload a sanitized pose video from the user's device.
```csharp
var poseVideo = File.Open("pose.mp4", FileMode.Open);
var resultOfPoseUpload = kycClient.UploadPoseVideo(poseVideo).Result;
```

Now it's time to obtain the validation code for speech validation. For more information,
refer to [Speech Validation In Kyc]()
```csharp
var validationCode = kycClient.GenerateValidateCode().Result;
```

Afterward, your client should capture a video of themselves and recite the code aloud,
which has already been sanitized on your client's device.
Subsequently, you should transmit the sanitized video to our server for the final step.
```csharp
var speechVideo = File.Open("pose.mp4", FileMode.Open);
var speechValidationResult = kycClient.UploadSpeechValidationVideo(speechVideo).Result;
```

> **Information:** Please note that if your client fails the KYC process,
> you should generate a new set of [Session Credentials]()
> for them, which includes a ``Session Token`` and an ``RSA Public Key``

``Kyc Sdk`` It employs [Asymmetric Encryption](https://en.wikipedia.org/wiki/Public-key_cryptography) 
with [RSA](https://en.wikipedia.org/wiki/RSA_(cryptosystem)). Consequently, 
all sensitive client information will be encrypted using the ``RSACryptoServiceProvider``.

