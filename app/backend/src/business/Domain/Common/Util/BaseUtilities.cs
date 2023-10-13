using System.Collections.ObjectModel;
using System.Reflection;
using System.Text;

namespace Domain.Common.Util;

public abstract class PaginationUtils
{
    public static void Paginate(out int skip, int perPage, int? page = null)
    {
        if (page == null)
        {
            skip = perPage;
            return;
        }

        skip = (page.Value - 1) * perPage;
    }
}

public static class StringUtils
{
    public static Stream GenerateStreamFromString(string s)
    {
        var stream = new MemoryStream();
        var writer = new StreamWriter(stream);
        writer.Write(s);
        writer.Flush();
        stream.Position = 0;
        return stream;
    }
}

public static class ArrayUtils
{
    public static string ArrayToString(ReadOnlyCollection<byte> arr)
    {
        var s = new StringBuilder(arr.Count * 2);
        foreach (var t in arr) s.AppendFormat("{0:x2}", t);

        return s.ToString();
    }
}

public class Compare
{
    public static IEnumerable<string> GetChangedPropertyNames<T, Tg>(T original, Tg changedObj,
        string[] excludes = null!) where T : class
        where Tg : class
    {
        var changes = new List<string>();
        IEnumerable<PropertyInfo> properties = typeof(T).GetProperties().ToList();
        foreach (var property in properties)
        {
            if (excludes.Any(item => item.ToLower().Contains(property.Name.ToLower())))
                continue;

            var originalValue = property.GetValue(original)?.ToString();
            var changedValue = property.GetValue(changedObj)?.ToString();
            if (originalValue != changedValue) changes.Add(property.Name);
        }

        return changes;
    }

    public static IEnumerable<string> GetChangedPropertyNames<T>(T original, T changedObj, string[] excludes = null!)
        where T : class
    {
        return GetChangedPropertyNames<T, T>(original, changedObj, excludes);
    }
}