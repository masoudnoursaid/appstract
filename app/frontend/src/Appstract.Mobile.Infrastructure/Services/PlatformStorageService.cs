using Appstract.Front.Application.Services;

namespace Appstract.Front.Mobile.Infrastructure.Services;
public class PlatformStorageService : IPlatformStorageService
{
    private readonly ISecureStorage _secureStorage;
    public PlatformStorageService(ISecureStorage secureStorage)
    {
        _secureStorage = secureStorage;
    }
    public Task ClearAsync(CancellationToken? cancellationToken = null)
    {
        _secureStorage.RemoveAll();
        return Task.CompletedTask;
    }

    public async Task<bool> ContainKeyAsync(string key, CancellationToken? cancellationToken = null)
    {
        string value = await _secureStorage.GetAsync(key);
        return value != "";
    }

    public Task<string> GetItemAsStringAsync(string key, CancellationToken? cancellationToken = null)
    {
        return _secureStorage.GetAsync(key);
    }

    public Task<T> GetItemAsync<T>(string key, CancellationToken? cancellationToken = null)
    {
        return null;
    }

    public Task<string> KeyAsync(int index, CancellationToken? cancellationToken = null)
    {
        return Task.FromResult("");
    }

    public Task<int> LengthAsync(CancellationToken? cancellationToken = null)
    {
        return Task.FromResult(0);
    }

    public Task RemoveItemAsync(string key, CancellationToken? cancellationToken = null)
    {
        _secureStorage.Remove(key);
        return Task.CompletedTask;
    }

    public Task SetItemAsStringAsync(string key, string data, CancellationToken? cancellationToken = null)
    {
        return _secureStorage.SetAsync(key, data);
    }

    public Task SetItemAsync<T>(string key, T data, CancellationToken? cancellationToken = null)
    {
        return Task.CompletedTask;
    }
}
