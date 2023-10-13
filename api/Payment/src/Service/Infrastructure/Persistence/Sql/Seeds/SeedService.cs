using Domain.Entities;
using Domain.Enums;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ApplicationEntity = Domain.Entities.Application;

namespace Infrastructure.Persistence.Sql.Seeds;

public interface ISeedService
{
    Task StartAll();
    Task SetupMerchants();
    Task SetupCurrencies();
    Task SetupProcessStatus();
    Task SetupPaymentMethod();
}

public class SeedService : ISeedService
{
    private static readonly string MerchantId = Guid.NewGuid().ToString();
    private readonly PaymentDbContext _dbContext;
    private readonly ILogger<SeedService> _logger;

    public SeedService(PaymentDbContext dbContext, ILogger<SeedService> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task StartAll()
    {
        await _dbContext.Database.EnsureDeletedAsync();
        await _dbContext.Database.MigrateAsync();
        await _dbContext.Database.EnsureCreatedAsync();
        _logger.LogInformation("SEED.START --- Db cleared.");

        await SetupMerchants();
        await SetupCurrencies();
        await SetupProcessStatus();
        await SetupPaymentMethod();
        await SetupApplication();
        await _dbContext.SaveChangesAsync();
        _logger.LogInformation("SEED.START.SAVE_DB --- DONE !.");
    }

    public async Task SetupMerchants()
    {
        var merchant = new MerchantOwner
        {
            Name = "mc",
            Email = "mc@email.com",
            Id = MerchantId
        };
        await _dbContext.MerchantOwners.AddAsync(merchant);
        _logger.LogInformation("SEED.SETUP_MERCHANTS --- Merchant done.");
    }

    public async Task SetupCurrencies()
    {
        var usd = new Currency
        {
            Title = "USD",
            Symbol = "$",
            FullName = "United State Dollar"
        };
        await _dbContext.Currencies.AddAsync(usd);

        var rm = new Currency
        {
            Title = "RM",
            Symbol = "RM",
            FullName = "Malaysian Ringgit"
        };
        await _dbContext.Currencies.AddAsync(rm);

        var eur = new Currency
        {
            Title = "EUR",
            Symbol = "EUR",
            FullName = "euro"
        };
        await _dbContext.Currencies.AddAsync(eur);


        _logger.LogInformation("SEED.SETUP_CURRENCIES --- Currencies done.");
    }

    public async Task SetupProcessStatus()
    {
        var created = new PaymentStatus
        {
            Title = "Payment Created",
            Description = "",
            ProcessStatus = PaymentProcessType.PaymentCreated
        };
        var verified = new PaymentStatus
        {
            Title = "Payment verified",
            Description = "",
            ProcessStatus = PaymentProcessType.PaymentSucceed
        };
        var rejected = new PaymentStatus
        {
            Title = "Payment rejected",
            Description = "",
            ProcessStatus = PaymentProcessType.PaymentRejected
        };
        var canceled = new PaymentStatus
        {
            Title = "Payment canceled",
            Description = "",
            ProcessStatus = PaymentProcessType.PaymentCanceled
        };
        await _dbContext.PaymentStatus.AddAsync(created);
        await _dbContext.PaymentStatus.AddAsync(verified);
        await _dbContext.PaymentStatus.AddAsync(rejected);
        await _dbContext.PaymentStatus.AddAsync(canceled);
        _logger.LogInformation("SEED.SETUP_PROCESS_STATUS --- ProcessStatus done.");
    }

    public async Task SetupPaymentMethod()
    {
        var paypal = new PaymentMethod
        {
            Title = "PayPal",
            Active = true,
            MerchantOwnerId = MerchantId,
            Icon = "",
            DisplayOrder = 1,
            DisplayTitle = "PayPal GetWay",
            Sandbox = true,
            GetWay = SourceImplementedGetWay.Paypal,
            Provider = "mc-provider",
            GeographicSanctions = new[]
            {
                new GeoLocation
                {
                    CountryName = "IR",
                    CountryCode = "IR-code"
                }
            },
            SupportedCountries = new[]
            {
                new GeoLocation
                {
                    CountryName = "US",
                    CountryCode = "US-code"
                }
            },
            GeographicRestrictionEnforced = true
        };
        await _dbContext.PaymentMethods.AddAsync(paypal);

        var billplz = new PaymentMethod
        {
            Title = "billplz",
            Active = true,
            MerchantOwnerId = MerchantId,
            Icon = "",
            DisplayOrder = 1,
            DisplayTitle = "PayPal GetWay",
            Sandbox = true,
            GetWay = SourceImplementedGetWay.Billplz,
            Provider = "mc-provider",
            GeographicSanctions = new[]
            {
                new GeoLocation
                {
                    CountryName = "IR",
                    CountryCode = "IR-code"
                }
            },
            SupportedCountries = new[]
            {
                new GeoLocation
                {
                    CountryName = "US",
                    CountryCode = "US-code"
                }
            },
            GeographicRestrictionEnforced = true
        };
        await _dbContext.PaymentMethods.AddAsync(billplz);

        var stripe = new PaymentMethod
        {
            Title = "stripe",
            Active = true,
            MerchantOwnerId = MerchantId,
            Icon = "",
            DisplayOrder = 1,
            DisplayTitle = "stripe GetWay",
            Sandbox = true,
            GetWay = SourceImplementedGetWay.Stripe,
            Provider = "mc-provider",
            GeographicSanctions = new[]
            {
                new GeoLocation
                {
                    CountryName = "IR",
                    CountryCode = "IR-code"
                }
            },
            SupportedCountries = new[]
            {
                new GeoLocation
                {
                    CountryName = "US",
                    CountryCode = "US-code"
                }
            },
            GeographicRestrictionEnforced = true
        };

        await _dbContext.PaymentMethods.AddAsync(stripe);
        
        var mollie = new PaymentMethod
        {
            Title = "mollie",
            Active = true,
            MerchantOwnerId = MerchantId,
            Icon = "",
            DisplayOrder = 1,
            DisplayTitle = "mollie GetWay",
            Sandbox = true,
            GetWay = SourceImplementedGetWay.Mollie,
            Provider = "mc-provider",
            GeographicSanctions = new[]
            {
                new GeoLocation
                {
                    CountryName = "IR",
                    CountryCode = "IR-code"
                }
            },
            SupportedCountries = new[]
            {
                new GeoLocation
                {
                    CountryName = "US",
                    CountryCode = "US-code"
                }
            },
            GeographicRestrictionEnforced = true
        };

        await _dbContext.PaymentMethods.AddAsync(mollie);

        _logger.LogInformation("SEED.SETUP_PAYMENT_METHOD --- PaymentMethod done.");
    }


    public async Task SetupApplication()
    {
        var application = new ApplicationEntity
        {
            Title = "Test",
            AuthorizedIpAddresses = new List<IpAddress> { IpAddress.Create("127.0.0.1") }
        };

        application.SetApiKey(ApplicationSeedStorage.Key);
        application.SetApiSecret(ApplicationSeedStorage.Secret);

        await _dbContext.Applications.AddAsync(application);
        _logger.LogInformation("SEED.SETUP_APPLICATION --- Application done.");
    }
}