using TechTalk.SpecFlow.Assist.ValueRetrievers;

namespace Appstract.AcceptanceTest.Hooks.ValueRetrievers;

public class NullableStringValueRetriever : ClassRetriever<string>
{
    protected override string GetNonEmptyValue(string value)
    {
        if (value == string.Empty)
        {
            return null!;
        }

        return value == "<empty>" ? string.Empty : value;
    }
}