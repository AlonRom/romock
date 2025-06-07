using System.Collections.ObjectModel;
using RoMock.Example.App.Models;
using RoMock.Example.App.Services.CommentService;
using RoMock.Example.App.ViewModels.Base;

namespace RoMock.Example.App.ViewModels;

public partial class CommentsViewModel : ViewModelBase
{
    private readonly ICommentService _commentService;
    public ObservableCollection<CommentModel>? Comments { get; set; } = [];

    public CommentsViewModel(ICommentService commentService)
    {
        _commentService = commentService;
    }
    public override async Task LoadAsync()
    {
        await Loading(
            async () =>
            {
                await GetComments();
            });
    }

    private async Task GetComments()
    {
        var comments = await _commentService.GetCommentsAsync();
        if (comments != null)
        {
            Comments?.Clear();
            foreach (var comment in comments)
            {
                Comments?.Add(comment);
            }
        }
    }
}