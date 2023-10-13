using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common.BaseTypes;
using Domain.ValueObjects;
using IPAddress = Domain.ValueObjects.IpAddress;


namespace Domain.Entities;

public class Application : BaseEntity
{
    [NotMapped] public ApiSecret? ApiSecret { get; private set; }

    [NotMapped] public ApiKey? ApiKey { get; private set; }

    [NotMapped]
    public IEnumerable<IpAddress>? AuthorizedIpAddresses
    {
        get
        {
            if (_authorizedIpAddresses == null) return null;
            var ips = _authorizedIpAddresses.Split(",");
            var result = ips.Select(IPAddress.Create);
            return result;
        }
        set
        {
            var ips = value!.Select(i => i.Ip).ToArray();
            _authorizedIpAddresses = string.Join(",", ips);
        }
    }

    [MaxLength(50)] public string? Title { get; set; }

    private string? _authorizedIpAddresses { get; set; }
    
    public ICollection<Payment>? Payments { get; set; }


    public void SetApiKey(ApiKey apiKey)
    {
        ApiKey = apiKey;
    }

    public void SetApiSecret(ApiSecret apiSecret)
    {
        ApiSecret = apiSecret;
    }
}