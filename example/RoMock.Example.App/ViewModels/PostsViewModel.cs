using System.Collections.ObjectModel;
using RoMock.Example.App.Models;
using RoMock.Example.App.Services.PostService;
using RoMock.Example.App.ViewModels.Base;

namespace RoMock.Example.App.ViewModels;

public partial class PostsViewModel : ViewModelBase
{
    private readonly IPostService _postService;
    public ObservableCollection<PostModel>? Posts { get; set; } = [];

    public PostsViewModel(IPostService postService)
    {
        _postService = postService;
    }
    public override async Task LoadAsync()
    {
        await Loading(
            async () =>
            {
                await GetPosts();
            });
    }

    private async Task GetPosts()
    {
        var posts = await _postService.GetPostsAsync();
        if (posts != null)
        {
            Posts?.Clear();
            foreach (var post in posts)
            {
                Posts?.Add(post);
            }
        }
    }
}