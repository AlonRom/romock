using RoMock.Example.App.ViewModels;
using RoMock.Example.App.Views.Base;

namespace RoMock.Example.App.Views;

public partial class UsersPage : ContentPageBase
{
    public UsersPage(UsersViewModel usersViewModel)
    {
        InitializeComponent();
        BindingContext = usersViewModel;
    }
}