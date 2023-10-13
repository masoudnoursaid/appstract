using System.Diagnostics.CodeAnalysis;
using Net.Codecrete.QrCodeGenerator;

namespace Appstract.Front.Application.Common.Extensions;

[ExcludeFromCodeCoverage]
public static class QrCodeExtension
{
    public static string ToSvgQrCode(this string text)
    {
        return QrCode.EncodeText(text, QrCode.Ecc.Medium).ToSvgString(4);
    }
}