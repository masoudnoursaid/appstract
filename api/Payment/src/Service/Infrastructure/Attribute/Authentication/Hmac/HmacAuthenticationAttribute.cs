using Microsoft.AspNetCore.Mvc;

namespace Infrastructure.Attribute.Authentication.Hmac;

public class HmacAuthenticationAttribute : TypeFilterAttribute
{
    public HmacAuthenticationAttribute() : base(typeof(HmacAuthRequiredFilter))
    {
    }
}