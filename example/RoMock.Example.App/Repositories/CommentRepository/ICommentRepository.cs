using RoMock.Example.App.Models;
using RoMock.Library.Interfaces;

namespace RoMock.Example.App.Repositories.CommentRepository;

public interface ICommentRepository : IMockable
{
    public Task<IEnumerable<CommentModel>?> GetCommentsAsync();
}

