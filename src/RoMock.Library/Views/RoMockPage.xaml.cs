using RoMock.Library.ViewModels;

namespace RoMock.Library.Views;

public partial class RoMockPage : ContentPage
{
    public RoMockPage(RoMockViewModel roMockViewModel)
    {
        InitializeComponent();
        BindingContext = roMockViewModel;
    }
}
