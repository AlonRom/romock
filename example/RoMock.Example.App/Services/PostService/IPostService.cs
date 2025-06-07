using RoMock.Example.App.Models;

namespace RoMock.Example.App.Services.PostService;

public interface IPostService
{
    public Task<IEnumerable<PostModel>?> GetPostsAsync();

    public Task<PostModel?> GetPostByIdAsync(string postId);
}