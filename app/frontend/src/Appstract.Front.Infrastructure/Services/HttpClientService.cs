namespace Appstract.Front.Infrastructure.Services
{
    public class HttpClientService
    {
        private readonly HttpClient _httpClient;
    
        public HttpClientService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public string GetBaseAddress()
        {
            return _httpClient.BaseAddress?.AbsoluteUri ?? string.Empty;
        }
    }
}