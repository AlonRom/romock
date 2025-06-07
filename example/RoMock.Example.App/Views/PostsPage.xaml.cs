using RoMock.Example.App.ViewModels;
using RoMock.Example.App.Views.Base;

namespace RoMock.Example.App.Views;

public partial class PostsPage : ContentPageBase
{
    public PostsPage(PostsViewModel postsViewModel)
    {
        InitializeComponent();
        BindingContext = postsViewModel;
    }
}