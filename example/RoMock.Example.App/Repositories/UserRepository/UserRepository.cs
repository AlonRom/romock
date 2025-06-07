using System.Net.Http.Json;
using RoMock.Example.App.Models;
using RoMock.Example.App.Repositories.Base;
using RoMock.Library.Attributes;

namespace RoMock.Example.App.Repositories.UserRepository;

public class UserRepository : RepositoryBase, IUserRepository
{
    public UserRepository(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
    {
    }

    [MockableMethod]
    public async Task<IEnumerable<UserModel>?> GetUsersAsync()
    {
        try
        {
            HttpClient httpClient = CreateHttpClient();
            IEnumerable<UserModel>? users =
                await httpClient.GetFromJsonAsync<IEnumerable<UserModel>>("users", JsonSerializerOptions);
            return users!;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }
}