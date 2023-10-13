using System.Security.Claims;
using Appstract.Front.Application.Common.Constants;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;

namespace Appstract.Front.Infrastructure.Services.Authentications;

public class AuthStateProvider : AuthenticationStateProvider
{
    private readonly ILocalStorageService _localStorage;
    private readonly AuthenticationState _anonymous;

    public AuthStateProvider(ILocalStorageService localStorage)
    {
        _localStorage = localStorage;
        _anonymous = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        string token = await _localStorage.GetItemAsStringAsync(UltraToneStorage.ACCESS_TOKEN);

        if (string.IsNullOrWhiteSpace(token))
        {
            return _anonymous;
        }

        ClaimsPrincipal claimsPrincipal = CreateClaimsPrincipal(token);
        return new AuthenticationState(claimsPrincipal);
    }

    private ClaimsPrincipal CreateClaimsPrincipal(string username)
    {
        IEnumerable<Claim> claims = new List<Claim>() { new(ClaimTypes.Name, username), };

        ClaimsIdentity claimsIdentity = new(claims, "jwtAuthType");
        ClaimsPrincipal claimsPrincipal = new(claimsIdentity);
        return claimsPrincipal;
    }
}