using RoMock.Example.App.ViewModels;
using RoMock.Example.App.Views.Base;

namespace RoMock.Example.App.Views;

public partial class LoginPage : ContentPageBase
{
    public LoginPage(LoginViewModel loginViewModel)
    {
        InitializeComponent();
        BindingContext = loginViewModel;
    }
}