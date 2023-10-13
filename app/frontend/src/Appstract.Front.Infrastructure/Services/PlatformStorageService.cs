using Appstract.Front.Application.Services;
using Blazored.LocalStorage;

namespace Appstract.Front.Infrastructure.Services
{
    public class PlatformStorageService : IPlatformStorageService
    {
        private readonly ILocalStorageService _localStorage;

        public PlatformStorageService(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }

        public async Task ClearAsync(CancellationToken? cancellationToken = null)
        {
            await _localStorage.ClearAsync();
        }

        public async Task<T> GetItemAsync<T>(string key, CancellationToken? cancellationToken = null)
        {
            return await _localStorage.GetItemAsync<T>(key);
        }

        public async Task<string> GetItemAsStringAsync(string key, CancellationToken? cancellationToken = null)
        {
            return await _localStorage.GetItemAsStringAsync(key);
        }

        public async Task<string> KeyAsync(int index, CancellationToken? cancellationToken = null)
        {
            return await _localStorage.KeyAsync(index);
        }

        public async Task<bool> ContainKeyAsync(string key, CancellationToken? cancellationToken = null)
        {
            return await _localStorage.ContainKeyAsync(key);
        }

        public async Task<int> LengthAsync(CancellationToken? cancellationToken = null)
        {
            return await _localStorage.LengthAsync();
        }

        public async Task RemoveItemAsync(string key, CancellationToken? cancellationToken = null)
        {
            await _localStorage.RemoveItemAsync(key);
        }

        public async Task SetItemAsync<T>(string key, T data, CancellationToken? cancellationToken = null)
        {
            await _localStorage.SetItemAsync(key,data);
        }

        public async Task SetItemAsStringAsync(string key, string data, CancellationToken? cancellationToken = null)
        {
            await _localStorage.SetItemAsStringAsync(key,data);
        }
    }
}