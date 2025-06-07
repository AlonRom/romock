using RoMock.Example.App.ViewModels;
using RoMock.Example.App.Views.Base;

namespace RoMock.Example.App.Views;

public partial class PostPage : ContentPageBase
{
    public PostPage(PostViewModel postViewModel)
    {
        InitializeComponent();
        BindingContext = postViewModel;
    }
}