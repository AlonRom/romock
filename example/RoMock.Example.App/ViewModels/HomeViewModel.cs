using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RoMock.Example.App.Services.NavigationService;
using RoMock.Example.App.ViewModels.Base;

namespace RoMock.Example.App.ViewModels;

public partial class HomeViewModel : ViewModelBase
{
    private readonly INavigationService _navigationService;

    [ObservableProperty]
    private string? _postId;

    public HomeViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;
    }

    [RelayCommand]
    private async Task NavigateToPostsAsync()
    {
        await _navigationService.GoToPostsPage();
    }

    [RelayCommand]
    private async Task NavigateToCommentsAsync()
    {
        await _navigationService.GoToCommentsPage();
    }

    [RelayCommand]
    private async Task NavigateToToDosAsync()
    {
        await _navigationService.GoToToDosPage();
    }

    [RelayCommand]
    private async Task NavigateToUsersAsync()
    {
        await _navigationService.GoToUsersPage();
    }

    [RelayCommand]
    private async Task NavigateToPostByIdAsync()
    {
        if (!string.IsNullOrEmpty(PostId))
        {
            await _navigationService.GoToPostPage(PostId);
        }
    }
}