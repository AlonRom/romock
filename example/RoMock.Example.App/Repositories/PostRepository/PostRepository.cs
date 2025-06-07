using System.Net.Http.Json;
using RoMock.Example.App.Models;
using RoMock.Example.App.Repositories.Base;
using RoMock.Library.Attributes;

namespace RoMock.Example.App.Repositories.PostRepository;

public class PostRepository : RepositoryBase, IPostRepository
{
    public PostRepository(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
    {
    }

    public async Task<IEnumerable<PostModel>?> GetPostsAsync()
    {
        try
        {
            HttpClient httpClient = CreateHttpClient();
            IEnumerable<PostModel>? posts = await httpClient.GetFromJsonAsync<IEnumerable<PostModel>>("posts", JsonSerializerOptions);
            return posts!;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }

    [MockableMethod]
    public async Task<PostModel?> GetPostByIdAsync(string postId)
    {
        try
        {
            HttpClient httpClient = CreateHttpClient();
            PostModel? post = await httpClient.GetFromJsonAsync<PostModel>($"posts/{postId}", JsonSerializerOptions);
            return post!;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }
}