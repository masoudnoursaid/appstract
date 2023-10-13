using Application.Common.Consts.Authentication;
using Application.Services.HttpAuth;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Services.HttpAuth
{
    public class HttpAuthService : IHttpAuthService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HttpAuthService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Task<string> GetApiKeyFromContext()
        {
            var result = _httpContextAccessor.HttpContext!.Request
                .Headers[HmacAuthentication.API_KEY_HEADER].ToString();

            return Task.FromResult(result);
        }
    }
}