using RoMock.Example.App.Models;
using RoMock.Library.Interfaces;

namespace RoMock.Example.App.Repositories.PostRepository;

public interface IPostRepository : IMockable
{
    public Task<IEnumerable<PostModel>?> GetPostsAsync();
    public Task<PostModel?> GetPostByIdAsync(string postId);
}