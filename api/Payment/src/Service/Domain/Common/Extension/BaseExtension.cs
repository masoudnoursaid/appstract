namespace Domain.Common.Extension;

public static class BaseExtension
{
    public static IEnumerable<T> JoinEach<T, Tg>(this IEnumerable<Tg> data, Func<Tg, Tg, T> make)
    {
        var enumerable = data.ToList();

        return (from item in enumerable from item2 in enumerable select make(item, item2)).ToList();
    }

    public static async Task<IEnumerable<TOut>> ForEach<T, TOut>(this IEnumerable<T> data, Func<T, Task<TOut>> func)
    {
        var list = new List<TOut>();
        foreach (var item in data)
        {
            var result = await func(item);
            list.Add(result);
        }

        return list;
    }

    public static void ForEach<T>(this IEnumerable<T> data, Action<T> action)
    {
        foreach (var item in data) action(item);
    }

    public static void For(this int num, Action f)
    {
        for (var i = 0; i < num; i++) f.Invoke();
    }
}