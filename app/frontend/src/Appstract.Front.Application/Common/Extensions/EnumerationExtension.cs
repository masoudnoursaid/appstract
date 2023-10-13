using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Resources;

namespace Appstract.Front.Application.Common.Extensions;

[ExcludeFromCodeCoverage]
public static class EnumerationExtension
{
    public static string ToDescriptionString(this Enum enumValue)
    {
        try
        {
            return enumValue.GetType()
                .GetMember(enumValue.ToString())
                .First()
                .GetCustomAttribute<DescriptionAttribute>(false)?
                .Description ?? enumValue.ToString().ToLower();
        }
        catch
        {
            return string.Empty;
        }
    }

    public static string ToDisplayString(this Enum enumValue)
    {
        DisplayAttribute? attribute = enumValue.GetType()
            .GetMember(enumValue.ToString())
            .First()
            .GetCustomAttribute<DisplayAttribute>(false);

        return attribute is null
            ? enumValue.ToString().ToLower()
            : new ResourceManager(attribute.ResourceType!).GetString(attribute.Name!)!;
    }
}