using System.Reflection;
using System.Text;
using ErrorHandling.Attributes;
using ErrorHandling.Enums;
using ErrorHandling.Helpers;
using Microsoft.OpenApi.Extensions;

namespace ErrorHandling.UnitTests;

/// <summary>
/// This tests created to ensure every error code has a corresponding error message.
/// </summary>
public class ErrorMessageTests
{
    private static List<Type> BackendErrorEnums => GetErrorEnumsOfAssemblyContaining(typeof(Application.DependencyInjection));

    [Fact]
    public void AllBackendErrorCodesEnumValuesHaveValidErrorMessages()
    {
        Dictionary<string, string> messages = GetAllErrorMessages();
        int count = 0;
        StringBuilder errors = new();

        foreach (Type enumTypeInfo in BackendErrorEnums)
        {
            foreach (Enum enumValue in enumTypeInfo.GetEnumValues())
            {
                BackendErrorType errorType = enumValue.GetAttributeOfType<ErrorTypeAttribute>().BackendErrorType;
                string valueAsFormattedString = Convert.ToUInt64(enumValue).ToString("##0'_'###'_'###");

                if (errorType != BackendErrorType.ApplicationFailure && errorType != BackendErrorType.ThirdPartyFailure &&
                    !messages.ContainsKey(valueAsFormattedString))
                {
                    errors.AppendLine(
                        $"Error code '{valueAsFormattedString} ({enumValue})' from '{enumTypeInfo.Name.Replace("ErrorCodes", string.Empty)}' of type '{errorType}' is missing.");
                    count++;
                }
            }
        }

        if (!string.IsNullOrEmpty(errors.ToString()))
        {
            errors.Insert(0, $"{count} Error message not prepared. \n");
            Assert.True(false, string.Join(Environment.NewLine, errors));
        }

        Assert.True(true);
    }

    [Fact]
    public void AllErrorMessagesHaveBackendErrorCodesEnumValues()
    {
        Dictionary<string, string> messages = GetAllErrorMessages();
        int count = 0;
        StringBuilder errors = new();
        List<string> backendErrorCodes = BackendErrorEnums
            .SelectMany(x => x.GetEnumValues().Cast<Enum>().Select(a => Convert.ToUInt64(a).ToString("##0'_'###'_'###"))).ToList();

        List<uint> commonErrorCodes = typeof(CommonErrorCode).GetEnumValues().Cast<Enum>().Select(Convert.ToUInt32).ToList();

        foreach (KeyValuePair<string, string> message in messages)
        {
            if (!backendErrorCodes.Contains(message.Key) && !commonErrorCodes.Any(x => message.Key.EndsWith($"_{x}")))
            {
                errors.AppendLine(
                    $"Message key '{message.Key}' not found in error codes.");
                count++;
            }
        }

        if (!string.IsNullOrEmpty(errors.ToString()))
        {
            errors.Insert(0, $"{count} Error message not found in any error code enums. \n");
            Assert.True(false, string.Join(Environment.NewLine, errors));
        }

        Assert.True(true);
    }

    private static List<Type> GetErrorEnumsOfAssemblyContaining(Type type)
    {
        return Assembly.GetAssembly(type)!
            .GetTypes()
            .Where(t => t.IsEnum && t.IsPublic && t.GetCustomAttribute<HandlerCodeAttribute>() is not null)
            .ToList();
    }

    private Dictionary<string, string> GetAllErrorMessages()
    {
        Assembly[] assemblies = new[]
        {
            typeof(Application.DependencyInjection).Assembly
        };

        Dictionary<string, string> result = new();

        foreach (Assembly assembly in assemblies)
        {
            Dictionary<string, string> errorMessages = ResourceHelper.GetAllErrorMessages(assembly);

            foreach (KeyValuePair<string, string> errorMessage in errorMessages)
            {
                result[errorMessage.Key] = errorMessage.Value;
            }
        }

        return result;
    }
}