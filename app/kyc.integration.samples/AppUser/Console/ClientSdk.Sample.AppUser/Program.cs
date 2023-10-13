using Client.SDK.Common.Enum;
using Client.SDK.DI;
using Client.SDK.Service.KycClient;
using Client.SDK.Service.Security;
using ClientSdk.AppUser.V1;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

// ReSharper disable UnusedVariable

namespace ClientSdk.Sample.AppUser;

public class Program
{
    public static void Main(string[] args)
    {
        HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);
        builder.Services.ConfigureKycClient(config =>
        {
            config.ConnectionConfiguration.Address = "https://localhost:44347";
            config.ConnectionConfiguration.Timeout = 10;
            config.SecurityConfiguration.Method = SecurityMethod.RSA;
        });

        var serviceProvider = builder.Services.BuildServiceProvider();
        var kycClient = serviceProvider.GetRequiredService<IKycClient>();
        var securityService = serviceProvider.GetRequiredService<ISecurityService>();

        var myTestPublicKey =
            "1U+Lay657nugTf4+t4htSydH5RwJ3EwXGjWEQt6K3k7Z2bVEziGqJEHAKl6u9zSzC6UMt5VNt6f17H0+ZYqOl4nOAGWlb+2iZjTwtuSz4iYZj1ZB5isse25br6rJ/zrbVayyB0wTHOOmWhvr9lPpWQfsm4kZ/GwF8GO0ckHedw4jaxXNguFQpoK5xxeG1MEZ8rNgczzfDHtNrn6w75x1zHR7jGoW32QqNGOhfMgT3vOOjD6/K91l3Gc+Fe+vm31Sp59P6cBSBEXmfzUL4fveL5311qN+Gq3BECw5zy6pQ/9ah0cv9V+2bymUN9zCFDcCDontptcsule4FTIVoG7BLQ==";
        var myTestToken = "1181c4467c9e8073d3451c24e493935089d17ac92067da1d572ab272b741f4ff";
        securityService.SeedSecurityPolicy(myTestToken, myTestPublicKey);
        
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
        
        
        var mrp = new UpdatePassportMrpRequest()
        {
            PrimarySide = "base64-img",
            MrpPayloadLine1 = "3902483>>>>>>>>3423>>>>>>342",
            MrpPayloadLine2 = "89689203>>>>>>>>343>>>>>>3209"
        };

        var resultOfUpdateMrp = kycClient.UpdatePassportMrp(mrp).Result;


        var poseVideo = File.Open("pose.mp4", FileMode.Open);
        var resultOfPoseUpload = kycClient.UploadPoseVideo(poseVideo).Result;

        var validationCode = kycClient.GenerateValidateCode().Result;

        var speechVideo = File.Open("pose.mp4", FileMode.Open);
        var speechValidationResult = kycClient.UploadSpeechValidationVideo(speechVideo).Result;


    }
}