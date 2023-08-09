using System.Reflection;
using Domain.Common.BaseTypes;
using ErrorHandling.Enums;
using ErrorHandling.Interfaces;
using Xunit.Sdk;

namespace ErrorHandling.UnitTests;

public class CommonErrorCodeTests
{
    [Fact]
    public void CommonErrorCode_enum_values_must_have_error_type_attribute()
    {
        foreach (Enum enumValue in typeof(CommonErrorCode).GetEnumValues())
        {
            TestHelper.CheckEnumValueHasErrorTypeAttribute(enumValue);
        }

        Assert.True(true);
    }

    [Fact]
    public void CommonErrorCode_enum_values_must_be_in_numerical_ascending_order()
    {
        TestHelper.CheckNumericalAscendingOrder(typeof(CommonErrorCode));
        Assert.True(true);
    }

    [Fact]
    public void CommonErrorCode_enum_values_must_be_between_600_and_999_inclusive()
    {
        foreach (Enum enumValue in typeof(CommonErrorCode).GetEnumValues())
        {
            int value = Convert.ToInt32(enumValue);

            if (value < 600 || value > 999)
            {
                throw new XunitException(
                    $"Value of '{value}' for '{enumValue.ToString()}' is unacceptable.\n\n" +
                    $"{nameof(CommonErrorCode)} enum values must be between 600 and 999 (inclusive).\n");
            }
        }

        Assert.True(true);
    }

    [Fact(Skip = "Object instantiation has issues")]
    public void Exceptions_must_not_share_a_common_error_code()
    {
        Dictionary<CommonErrorCode, Type> handlerCodes = new();

        IEnumerable<Type> codedExceptions = GetCodedExceptions();

        foreach (Type exceptionType in codedExceptions)
        {
            MethodInfo codeMethod =
                exceptionType.GetMethod(nameof(ICodedException.GetCommonErrorCode), BindingFlags.Public | BindingFlags.Instance)!;

            object instance = TestHelper.CreateInstanceWithDefaultValues(exceptionType);
            CommonErrorCode code = (CommonErrorCode)codeMethod.Invoke(instance, null)!;

            try
            {
                handlerCodes.Add(code, exceptionType);
            }
            catch (ArgumentException)
            {
                throw new XunitException(
                    $"'{code.ToString()}' error code cannot be reused by '{exceptionType.FullName}' as it's already been used by '{handlerCodes[code].FullName}'.");
            }
        }
    }

    private IEnumerable<Type> GetCodedExceptions()
    {
        List<Type> codedExceptions = new();

        codedExceptions.AddRange(GetCodedExceptionsInAssembly(typeof(Application.DependencyInjection).Assembly));
        codedExceptions.AddRange(GetCodedExceptionsInAssembly(typeof(Infrastructure.DependencyInjection).Assembly));
        codedExceptions.AddRange(GetCodedExceptionsInAssembly(typeof(IAggregateRoot).Assembly));

        return codedExceptions;
    }

    private static List<Type> GetCodedExceptionsInAssembly(Assembly assembly)
    {
        return assembly
            .GetTypes()
            .Where(t => t.IsClass && t.IsPublic && t.GetInterfaces().Any(i => i == typeof(ICodedException)))
            .OrderBy(t => t.FullName)
            .ToList();
    }
}