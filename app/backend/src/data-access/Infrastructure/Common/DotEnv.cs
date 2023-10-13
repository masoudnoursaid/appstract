namespace Infrastructure.Common;

public static class DotEnv
{
    public static void Load(string filePath)
    {
        if (!File.Exists(filePath)) return;

        foreach (var line in File.ReadAllLines(filePath))
        {
            if (line.StartsWith("#"))
                continue;

            var parts = line.Split(
                '=',
                2,
                StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

            if (string.IsNullOrEmpty(line)) continue;

            if (parts.Length != 2) continue;
            parts[1] = parts[1].Trim('"');

            Environment.SetEnvironmentVariable(parts[0], parts[1]);
        }
    }
}