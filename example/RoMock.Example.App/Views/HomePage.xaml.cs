using RoMock.Example.App.ViewModels;
using RoMock.Example.App.Views.Base;

namespace RoMock.Example.App.Views;

public partial class HomePage : ContentPageBase
{
    public HomePage(HomeViewModel homeViewModel)
    {
        InitializeComponent();
        BindingContext = homeViewModel;
    }
}