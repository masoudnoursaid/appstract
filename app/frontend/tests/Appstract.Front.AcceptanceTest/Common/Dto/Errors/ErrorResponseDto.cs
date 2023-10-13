namespace Appstract.AcceptanceTest.Common.Dto.Errors;

public class ErrorResponseDto
{
    public bool Success { get; set; } 
    public string ErrorCode { get; set; } = string.Empty;
}