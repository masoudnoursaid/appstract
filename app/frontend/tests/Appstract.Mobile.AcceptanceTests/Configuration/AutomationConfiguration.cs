namespace Appstract.Mobile.AcceptanceTests.Configuration;

public class AutomationConfiguration
{
    public required IosMacAutomationConfiguration IosMac { get; set; }
    public required AndroidAutomationConfiguration Android { get; set; }
}