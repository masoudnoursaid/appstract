namespace Application.Services.HttpAuth;

public interface IHttpAuthService
{
    Task<string> GetApiKeyFromContext();
}