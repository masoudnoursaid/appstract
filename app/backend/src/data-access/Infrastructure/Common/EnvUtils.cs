namespace Infrastructure.Common;

public class EnvUtils
{
    /// <summary>
    ///     Follow the .env.template and add .env file in your project root and in bin folder of your project
    /// </summary>
    public static void SetupEnvFile()
    {
        var envFilePath = Path.Combine(Directory.GetCurrentDirectory(), ".env");
        DotEnv.Load(envFilePath);
    }
}