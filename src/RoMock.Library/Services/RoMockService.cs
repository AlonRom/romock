using System.Reflection;
using System.Text.Json;

namespace RoMock.Library.Services;

public class RoMockService : IRoMockService
{
    private readonly HttpClient _httpClient;
    private readonly IRoMockExecutor _roMockExecutor;

    public RoMockService(HttpClient httpClient,
        IRoMockExecutor roMockExecutor)
    {
        _httpClient = httpClient;
        _roMockExecutor = roMockExecutor;
    }

    public void RegisterMockMethod(string? methodName, MethodInfo? method)
    {
        _roMockExecutor.RegisterMockMethod(methodName, async _ => await ExecuteMockMethod(methodName, method));
    }

    public async Task<object> ExecuteMockMethod(string? methodName, MethodInfo? method)
    {
        try
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"api/{methodName}");
            response.EnsureSuccessStatusCode();

            var returnType = method?.ReturnType.GetGenericArguments().FirstOrDefault() ?? method?.ReturnType;
            if (returnType != null)
            {
                var result = await DeserializeResponse(response, returnType);
                return result;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }

        return null;
    }

    public async Task<object> DeserializeResponse(HttpResponseMessage response, Type returnType)
    {
        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize(content, returnType, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
    }
}