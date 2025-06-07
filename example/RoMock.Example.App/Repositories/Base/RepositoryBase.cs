using System.Text.Json;

namespace RoMock.Example.App.Repositories.Base;

public abstract class RepositoryBase
{
    protected readonly IHttpClientFactory HttpClientFactory;
    protected readonly JsonSerializerOptions JsonSerializerOptions = new(JsonSerializerDefaults.Web);

    protected RepositoryBase(IHttpClientFactory httpClientFactory)
    {
        HttpClientFactory = httpClientFactory;
    }

    protected HttpClient CreateHttpClient()
    {
        return HttpClientFactory.CreateClient(MauiProgram.JsonPlaceholderClient);
    }
}