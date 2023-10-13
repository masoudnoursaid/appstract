using ClientSdk.Sample.AppApi.Models;

namespace ClientSdk.Sample.AppApi;

public static class LocalStorage
{
    private static readonly List<KycModel> KycStorage = new();

    public static Task<KycModel> GetByToken(string token) => Task.FromResult(KycStorage.Single(k => k.Token == token));
    public static void Add(KycModel model) => KycStorage.Add(model);
}

