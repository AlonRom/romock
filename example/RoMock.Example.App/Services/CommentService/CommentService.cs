using RoMock.Example.App.Models;
using RoMock.Example.App.Repositories.CommentRepository;

namespace RoMock.Example.App.Services.CommentService;

public class CommentService : ICommentService
{
    private readonly ICommentRepository _commentRepository;

    public CommentService(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }

    public async Task<IEnumerable<CommentModel>?> GetCommentsAsync()
    {
        return await _commentRepository.GetCommentsAsync();
    }
}