using System.Net.Http.Json;
using RoMock.Example.App.Models;
using RoMock.Example.App.Repositories.Base;
using RoMock.Library.Attributes;

namespace RoMock.Example.App.Repositories.CommentRepository;

public class CommentRepository : RepositoryBase, ICommentRepository
{
    public CommentRepository(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
    {
    }

    [MockableMethod]
    public async Task<IEnumerable<CommentModel>?> GetCommentsAsync()
    {
        try
        {
            HttpClient httpClient = CreateHttpClient();
            IEnumerable<CommentModel>? comments = await httpClient.GetFromJsonAsync<IEnumerable<CommentModel>>("comments", JsonSerializerOptions);
            return comments!;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }
}