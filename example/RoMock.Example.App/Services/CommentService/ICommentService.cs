using RoMock.Example.App.Models;

namespace RoMock.Example.App.Services.CommentService;

public interface ICommentService
{
    public Task<IEnumerable<CommentModel>?> GetCommentsAsync();
}