using System.Collections.ObjectModel;
using System.Text;

namespace Payment.Common.SDK.Utilities;

public class PaginationUtils
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
        for (var i = 0; i < arr.Count; ++i) s.AppendFormat("{0:x2}", arr[i]);

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
        var properties = typeof(T).GetProperties().ToList();
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

public static class ByteUtils
{
    public static byte[] ToByteArray(this Stream input)
    {
        var buffer = new byte[16 * 1024];
        using (var ms = new MemoryStream())
        {
            int read;
            while ((read = input.Read(buffer, 0, buffer.Length)) > 0) ms.Write(buffer, 0, read);
            return ms.ToArray();
        }
    }
}