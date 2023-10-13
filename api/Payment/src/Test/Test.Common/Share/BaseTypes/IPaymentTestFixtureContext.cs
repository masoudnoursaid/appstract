namespace Test.Common.Share.BaseTypes;

public interface IPaymentTestFixtureContext : ITestFixtureContext
{
    string MethodId { get; }
    void SetMethodId(string paymentMethodId);
}