using Appstract.Mobile.Application.Services;

namespace Appstract.Front.Mobile.Common;

public class NavigationService : INavigationService
{
    public static bool IsUnitTest = false;

    public T? GetPageViewModel<T>() where T : new()
    {
        Page? pageDetails =
            Shell.Current.CurrentItem.CurrentItem.Stack.FirstOrDefault(f =>
                f != null && f.BindingContext.GetType() == typeof(T));
        if (pageDetails != null)
        {
            return (T)pageDetails.BindingContext;
        }

        return default;
    }

    public async Task GoBack()
    {
        await Shell.Current.GoToAsync("..");
    }

    public Task NavigateToAsync(string route, IDictionary<string, object>? parameters = null)
    {
        if (IsUnitTest)
        {
            return Task.CompletedTask;
        }

        return parameters != null ? Shell.Current.GoToAsync(route, parameters) : Shell.Current.GoToAsync(route);
    }
}