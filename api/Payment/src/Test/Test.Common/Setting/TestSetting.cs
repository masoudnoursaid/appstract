using Microsoft.Extensions.Configuration;

namespace Test.Common.Setting;

public class TestSetting
{
    [ConfigurationKeyName("TEST_SEED_UP")] public bool SeedUp { get; set; }

    [ConfigurationKeyName("TEST_DB_CLEAN_UP")]
    public bool DbCleanUp { get; set; }

    [ConfigurationKeyName("TEST_INSECURE")]
    public bool Insecure { get; set; }
}