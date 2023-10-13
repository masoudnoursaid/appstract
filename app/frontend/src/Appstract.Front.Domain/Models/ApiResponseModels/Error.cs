using Appstract.Front.Domain.Enums;

namespace Appstract.Front.Domain.Models.ApiResponseModels;

public class Error
{
    public int Code { get; set; }
    public string FormattedCode => Convert.ToInt32(Code).ToString("#,##0").Replace(',', '_');
    public int Type { get; set; }
    public string Message { get; set; } = string.Empty;
    public AlertType FormattedType
    {
        get
        {
            switch (Type)
            {
                case 1:
                    return AlertType.Warning;
                case 2:
                    return AlertType.Error;
                default:
                    return AlertType.Error;
            }
        }
    }
}