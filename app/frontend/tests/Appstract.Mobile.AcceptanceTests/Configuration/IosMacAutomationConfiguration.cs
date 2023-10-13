namespace Appstract.Mobile.AcceptanceTests.Configuration;

public class IosMacAutomationConfiguration
{
    public required string PlatformVersion { get; set; }
    public required string DeviceName { get; set; }
    public bool ShowXcodeLog { get; set; }
    public bool ShowIosLog { get; set; }
    public bool NoReset { get; set; }
    public bool FullReset { get; set; }
}