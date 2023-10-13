# Kyc.App.Sdk  [![.NET](https://img.shields.io/badge/--512BD4?logo=.net&logoColor=ffffff)](https://dotnet.microsoft.com/)
![version](https://img.shields.io/badge/version-1.0.0-blue)

# Installation
> nuget install Kyc.Server.Sdk -Source "GitLab"

> nuget source Add -Name "GitLab" -Source "https://gitlab-registry.maxtld.com/api/v4/projects/25/packages/nuget/index.json" -UserName <your_username> -Password <your_token>


# Introduction
The KYC SDK provides a straightforward method to integrate your application with the KYC process, 
allowing you to authenticate your users' passports and faces to ensure that your clients
are authorized by approved-recognized issuers, like government. 
Visit the [official website]() for more information

# Get Started
Begin by installing the KYC Nuget Package from [here]()
, and then add the KYC service to your service collection:
```csharp
builder.Services.ConfigureKycClient(opt =>
{
    opt.ConnectionConfiguration.Address = "https://kyc_domain.com";
    opt.ConnectionConfiguration.Webhooks = new[] { "My_Webhook" };
    opt.SecurityConfiguration.ApiSecret = "api_secret_of_your_application";
    opt.SecurityConfiguration.ApiKey = "api_key_of_your_application";
    opt.SecurityConfiguration.Method = SecurityMethod.HMAC;
    // leave it null if you have no restrictions
    opt.SecurityConfiguration.BanAccountAfterSpecificNumbersOfTry = 5;
});
```
> **Warning:** We highly recommend placing your ApiSecret and ApiKey in your .env file.
> For additional information, please refer to [this link](https://en.wikipedia.org/wiki/Environment_variable)

After configuring your service, let's proceed to set up the KYC middleware:
```csharp
// protect your web hook url(s), which setted up at ConfigureKycClient
app.UseKycCallBackAuthentication();
```
Congratulations! You have successfully set up everything you need for the KYC process.
Now, you should inject the IKycClient into your class or handler. With this, 
you can generate a valid token for your users to proceed with the KYC process:
```csharp
public class MyClassOrHandler
{
    private readonly IKycClient _kycClient;

    public MyClassOrHandler(IKycClient kycClient)
    {
        _kycClient = kycClient;
    }

    public async Task<IActionResult> Index()
    {
        // Always create unique client id for your each clients
        string clientId = Guid.NewGuid().ToString();

        // Generate session with client id
        var result = await _kycClient.GenerateSession(clientId);

        if (result.Success)
        {
            DateTime expiredDate = DateTime.UtcNow.AddDays(result.Data!.ExpireTimeInDays);
            
            // Here is the token for your client
            string token = result.Data.Token;
            
            // Public key of Rsa encryption, please pass this to your client too
            string publicKey = result.Data.PublicKey;
        }
        else
        {
            AppApiClientErrorCodes code = result.Error!.Code;
            Dictionary<string, string> values = result.Error.Values!;
        }
    }
}
```
> **Information:**We highly recommend generating a unique ID for your user and then sending this ID via ClientId.
> You can find information on generating a unique id for your client [here](https://en.wikipedia.org/wiki/Universally_unique_identifier).

Following this step, you can provide the token to your client, 
who will use it to proceed with the KYC process. 
Once the process is complete, KYC will notify you via your webhook:
```csharp
public class MyWebhookController
{
    [Route("My_Webhook")]
    public Task<IActionResult> WebHook(string token, KycProcessStatus status)
    {
        // First validate token
        KycModel model = await LocalStorage.GetByToken(token);
        model.status = status;

        // Then check the status
        switch (status)
        {
            case KycProcessStatus.Fail:
                // Sorry! but your client did not successfully pass the KYC process.
                // Please create another session for your client
                break;
            case KycProcessStatus.Timeout:
                // Sorry! The token has expired, kindly create a new session for your client.
                break;
            case KycProcessStatus.Invalid:
                // Oops! We are experiencing server issues. Please wait for another callback.
                break;
            case KycProcessStatus.Verified:
                // Congratulations! Your client has been successfully approved!
                break;
        }

        return Ok();
    }
}
```

By including ``app.UseKycCallBackAuthentication();`` in your app builder, 
the KYC middleware will automatically authenticate any request to your webhook using 
either [HMAC Authentication](https://en.wikipedia.org/wiki/HMAC) or [RSA Authentication](https://en.wikipedia.org/wiki/RSA_Security), 
based on the configuration you set with
``opt.SecurityConfiguration.Method = SecurityMethod.HMAC; // or SecurityMethod.RSA;``

