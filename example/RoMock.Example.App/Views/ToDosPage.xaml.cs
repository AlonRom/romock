using RoMock.Example.App.ViewModels;
using RoMock.Example.App.Views.Base;

namespace RoMock.Example.App.Views;

public partial class ToDosPage : ContentPageBase
{
    public ToDosPage(ToDosViewModel doDosViewModel)
    {
        InitializeComponent();
        BindingContext = doDosViewModel;
    }
}