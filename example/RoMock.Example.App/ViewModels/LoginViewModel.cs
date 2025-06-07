using CommunityToolkit.Mvvm.Input;
using RoMock.Example.App.Services.NavigationService;
using RoMock.Example.App.ViewModels.Base;

namespace RoMock.Example.App.ViewModels;

public partial class LoginViewModel : ViewModelBase
{
    private readonly INavigationService _navigationService;

    public LoginViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;
    }   

    [RelayCommand]
    private async Task NavigateToRoMockAsync()
    {
        await _navigationService.GoToRoMock();
    }  
    
    [RelayCommand]
    private async Task LoginAsync()
    {
        await _navigationService.GoToHome();
    }
}