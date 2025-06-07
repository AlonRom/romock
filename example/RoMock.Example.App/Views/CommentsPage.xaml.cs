using RoMock.Example.App.ViewModels;
using RoMock.Example.App.Views.Base;

namespace RoMock.Example.App.Views;

public partial class CommentsPage : ContentPageBase
{
    public CommentsPage(CommentsViewModel commentsViewModel)
    {
        InitializeComponent();
        BindingContext = commentsViewModel;
    }
}