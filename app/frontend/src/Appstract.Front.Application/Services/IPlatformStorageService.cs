namespace Appstract.Front.Application.Services;

public interface IPlatformStorageService
{
    Task ClearAsync(CancellationToken? cancellationToken = null);

    Task<T> GetItemAsync<T>(string key, CancellationToken? cancellationToken = null);

    Task<string> GetItemAsStringAsync(
        string key,
        CancellationToken? cancellationToken = null);

    Task<string> KeyAsync(int index, CancellationToken? cancellationToken = null);

    Task<bool> ContainKeyAsync(string key, CancellationToken? cancellationToken = null);

    Task<int> LengthAsync(CancellationToken? cancellationToken = null);

    Task RemoveItemAsync(string key, CancellationToken? cancellationToken = null);

    Task SetItemAsync<T>(string key, T data, CancellationToken? cancellationToken = null);

    Task SetItemAsStringAsync(
        string key,
        string data,
        CancellationToken? cancellationToken = null);
}