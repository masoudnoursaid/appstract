using Appstract.AcceptanceTest.Hooks.ValueRetrievers;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using Xunit;

[assembly: CollectionBehavior(DisableTestParallelization = true)]

namespace Appstract.AcceptanceTest.Hooks
{
    [Binding]
    public class Hooks
    {
        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            Service.Instance.ValueRetrievers.Register(new NullableStringValueRetriever());
        }
    }
}
