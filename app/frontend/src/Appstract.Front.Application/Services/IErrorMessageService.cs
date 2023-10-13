namespace Appstract.Front.Application.Services;

public interface IErrorMessageService
{
    Task<string> GetErrorMessageAsync(string errorCode);
    Task<IDictionary<string, string>?> GetErrorMessageList();
}