namespace Appstract.Mobile.Application.Services;

public interface INavigationService
{
    T GetPageViewModel<T>() where T : new();

    Task GoBack();

    Task NavigateToAsync(string route, IDictionary<string, object>? parameters = null);
}