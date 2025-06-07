using System.Collections.ObjectModel;
using RoMock.Example.App.Models;
using RoMock.Example.App.Services.UserService;
using RoMock.Example.App.ViewModels.Base;

namespace RoMock.Example.App.ViewModels;

public partial class UsersViewModel : ViewModelBase
{
    private readonly IUserService _userService;
    public ObservableCollection<UserModel>? Users { get; set; } = [];

    public UsersViewModel(IUserService userService)
    {
        _userService = userService;
    }
    public override async Task LoadAsync()
    {
        await Loading(
            async () =>
            {
                await GetUsers();
            });
    }

    private async Task GetUsers()
    {
        var users = await _userService.GetUsersAsync();
        if (users != null)
        {
            Users?.Clear();
            foreach (var user in users)
            {
                Users?.Add(user);
            }
        }
    }
}