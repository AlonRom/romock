using RoMock.Example.App.Models;
using RoMock.Example.App.Repositories.PostRepository;

namespace RoMock.Example.App.Services.PostService;

public class PostService : IPostService
{
    private readonly IPostRepository _postRepository;

    public PostService(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }

    public async Task<IEnumerable<PostModel>?> GetPostsAsync()
    {
        return await _postRepository.GetPostsAsync();
    }

    public async Task<PostModel?> GetPostByIdAsync(string postId)
    {
        return await _postRepository.GetPostByIdAsync(postId);
    }
}