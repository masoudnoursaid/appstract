using System.Reflection;
using ErrorHandling.Attributes;
using ErrorHandling.Exceptions;
using Xunit.Sdk;

namespace ErrorHandling.UnitTests;

internal static class TestHelper
{
    public static void CheckNumericalAscendingOrder(Type enumTypeInfo)
    {
        Array enumValues = enumTypeInfo.GetEnumValues();
        FieldInfo[] fieldInfos = enumTypeInfo.GetFields(BindingFlags.Public | BindingFlags.Static);

        for (int i = 0; i < enumValues.Length; i++)
        {
            Enum enumValue = (Enum)enumValues.GetValue(i)!;
            FieldInfo fieldInfo = fieldInfos[i];

            if (enumValue.ToString() != fieldInfo.Name)
            {
                throw new XunitException(
                    enumTypeInfo.Name + " enum values must be in numerical ascending order.\n" +
                    $"Value of '{enumValue.ToString()}' ({Convert.ToInt32(enumValue)}) is less than its previous field.");
            }
        }
    }

    public static void CheckEnumValueHasErrorTypeAttribute(Enum enumValue)
    {
        Type enumType = enumValue.GetType();
        FieldInfo fieldInfo = enumType.GetField(enumValue.ToString())!;
        ErrorTypeAttribute? errorTypeAttribute = fieldInfo.GetCustomAttribute<ErrorTypeAttribute>();

        if (errorTypeAttribute is null)
        {
            throw new ErrorTypeAttributeIsMissingException(enumValue);
        }
    }

    public static object CreateInstanceWithDefaultValues(Type type)
    {
        if (type.GetConstructor(Type.EmptyTypes) != null)
        {
            return Activator.CreateInstance(type)!;
        }

        ConstructorInfo constructor = type.GetConstructors().FirstOrDefault()!;

        object[] defaultValues = constructor.GetParameters()
            .Select(param => GetDefaultValue(param.ParameterType))
            .ToArray()!;

        return Activator.CreateInstance(type, defaultValues)!;
    }

    private static object? GetDefaultValue(Type type)
    {
        return type.IsValueType ? Activator.CreateInstance(type) : null;
    }
}