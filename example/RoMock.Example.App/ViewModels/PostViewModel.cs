using CommunityToolkit.Mvvm.ComponentModel;
using RoMock.Example.App.Models;
using RoMock.Example.App.Services.PostService;
using RoMock.Example.App.ViewModels.Base;

namespace RoMock.Example.App.ViewModels;

public partial class PostViewModel : ViewModelBase, IQueryAttributable
{
    private readonly IPostService _postService;

    [ObservableProperty]
    private string? _id;

    [ObservableProperty]
    private PostModel? _post;

    public PostViewModel(IPostService postService)
    {
        _postService = postService;
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        var postId = query["PostId"] as string;
        if (!string.IsNullOrEmpty(postId))
        {
            Id = postId;
        }
    }

    public override async Task LoadAsync()
    {
        await Loading(
            async () =>
            {
                if(!string.IsNullOrEmpty(Id))
                {
                    await GetPostById(Id);
                }   
            });
    }

    private async Task GetPostById(string postId)
    {
        Post = await _postService.GetPostByIdAsync(postId);  
    }
}