using System.Reflection;
using ErrorHandling.Attributes;
using ErrorHandling.Enums;
using MediatR;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace ErrorHandling.UnitTests;

public class ErrorCodesTests
{
    private readonly ITestOutputHelper _testOutputHelper;

    public ErrorCodesTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public void All_backend_error_codes_enum_values_must_have_error_type_attribute()
    {
        foreach (Type enumTypeInfo in GetBackendErrorsEnums())
        {
            foreach (Enum enumValue in enumTypeInfo.GetEnumValues())
            {
                TestHelper.CheckEnumValueHasErrorTypeAttribute(enumValue);
            }
        }

        Assert.True(true);
    }

    [Fact]
    public void HandlerCode_enum_values_must_be_in_numerical_ascending_order()
    {
        TestHelper.CheckNumericalAscendingOrder(typeof(HandlerCode));

        Assert.True(true);
    }

    [Fact]
    public void ErrorCodes_enums_values_must_be_in_numerical_ascending_order()
    {
        foreach (Type errorsEnum in GetBackendErrorsEnums())
        {
            TestHelper.CheckNumericalAscendingOrder(errorsEnum);
        }

        Assert.True(true);
    }

    [Fact]
    public void ErrorCodes_enums_values_must_start_with_handler_code()
    {
        foreach (Type errorsEnum in GetBackendErrorsEnums())
        {
            HandlerCodeAttribute handlerCodeAttribute = errorsEnum.GetCustomAttribute<HandlerCodeAttribute>()!;
            int handlerCode = Convert.ToInt32(handlerCodeAttribute.HandlerCode);

            foreach (Enum enumValue in errorsEnum.GetEnumValues())
            {
                int enumValueInt = Convert.ToInt32(enumValue);
                int diff = enumValueInt - (handlerCode * 1000);

                if (diff < 0 || diff > 999)
                {
                    throw new XunitException(
                        $"'{enumValueInt}' is not acceptable as value of '{enumValue.ToString()}' in '{errorsEnum}'.\n\n" +
                        $"{errorsEnum.Name} enum values must start with '{handlerCode}'.\n");
                }
            }
        }

        Assert.True(true);
    }

    [Fact]
    public void ErrorCodes_enums_values_last_3_digits_must_be_between_100_and_200_exclusive()
    {
        foreach (Type errorsEnum in GetBackendErrorsEnums())
        {
            foreach (Enum enumValue in errorsEnum.GetEnumValues())
            {
                int last3Digits = Convert.ToInt32(enumValue) % 1000;

                if (last3Digits <= 100 || last3Digits >= 200)
                {
                    throw new XunitException(
                        $"'{last3Digits}' is not acceptable as last 3 digits of '{enumValue.ToString()}' in '{errorsEnum}'.\n\n" +
                        "Last 3 digits of all handler error codes enums must be between 100 and 200 (exclusive).\n");
                }
            }
        }

        Assert.True(true);
    }

    [Fact]
    public void Error_code_enums_must_not_share_a_handler_code()
    {
        Dictionary<HandlerCode, Type> handlerCodes = new();

        foreach (Type requestType in GetBackendErrorsEnums())
        {
            HandlerCodeAttribute handlerCodeAttribute = requestType.GetCustomAttribute<HandlerCodeAttribute>()!;
            HandlerCode code = handlerCodeAttribute.HandlerCode;

            try
            {
                handlerCodes.Add(code, requestType);
            }
            catch (ArgumentException)
            {
                throw new XunitException(
                    $"'{code.ToString()}' handler code cannot be used by '{requestType.FullName}' as it's already been used by '{handlerCodes[code].FullName}'.");
            }
        }
    }

    [Fact]
    public void All_handler_requests_must_have_HandlerCode_attribute()
    {
        uint requestsNotImplementedICodedRequestCount = 0;

        foreach (Type requestType in GetHandlerRequests())
        {
            if (requestType.GetCustomAttribute<HandlerCodeAttribute>() is null)
            {
                requestsNotImplementedICodedRequestCount++;
                _testOutputHelper.WriteLine(requestType.FullName);
            }
        }

        Assert.False(
            requestsNotImplementedICodedRequestCount == 1,
            requestsNotImplementedICodedRequestCount + $"The following request has not attribute '{nameof(HandlerCodeAttribute)}'");

        Assert.True(
            requestsNotImplementedICodedRequestCount == 0,
            requestsNotImplementedICodedRequestCount + $" requests that are listed below have not implemented {nameof(HandlerCodeAttribute)}'");
    }

    [Fact]
    public void Requests_must_not_share_a_handler_code()
    {
        Dictionary<HandlerCode, Type> handlerCodes = new();

        foreach (Type requestType in GetCodedRequests())
        {
            HandlerCode code = requestType.GetCustomAttribute<HandlerCodeAttribute>()!.HandlerCode;

            try
            {
                handlerCodes.Add(code, requestType);
            }
            catch (ArgumentException)
            {
                throw new XunitException(
                    $"'{code.ToString()}' handler code cannot be used by '{requestType.FullName}' as it's already been used by '{handlerCodes[code].FullName}'.");
            }
        }
    }

    private static List<Type> GetBackendErrorsEnums()
    {
        return GetApplicationAssembly()
            .GetTypes()
            .Where(t => t.IsEnum && t.IsPublic && t.GetCustomAttribute<HandlerCodeAttribute>() is not null)
            .ToList();
    }

    private IEnumerable<Type> GetHandlerRequests()
    {
        return GetApplicationAssembly()
            .GetTypes()
            .Where(t => t.IsClass && t.IsPublic &&
                        t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IRequestHandler<,>)))
            .Select(t => t.GetInterfaces().First().GetGenericArguments()[0])
            .OrderBy(t => t.FullName)
            .ToList();
    }

    private IEnumerable<Type> GetCodedRequests()
    {
        return GetApplicationAssembly()
            .GetTypes()
            .Where(t => t.IsClass && t.IsPublic &&
                        t.GetInterfaces().Any(i =>
                            i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IRequest<>) &&
                            t.GetCustomAttribute<HandlerCodeAttribute>() is not null))
            .OrderBy(t => t.FullName)
            .ToList();
    }

    private static Assembly GetApplicationAssembly()
    {
        return Assembly.GetAssembly(typeof(Application.DependencyInjection))!;
    }
}