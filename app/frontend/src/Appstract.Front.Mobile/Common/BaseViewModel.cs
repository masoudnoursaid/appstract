using Appstract.Mobile.Application.Services;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Appstract.Front.Mobile.Common;

public partial class BaseViewModel : ObservableObject
{
    protected readonly INavigationService NavigationService;

    public BaseViewModel(INavigationService navigationService)
    {
        NavigationService = navigationService;
    }

    [ObservableProperty]
    private bool _isBusy;
}