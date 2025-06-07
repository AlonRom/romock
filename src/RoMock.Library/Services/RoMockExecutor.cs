namespace RoMock.Library.Services;

public class RoMockExecutor : IRoMockExecutor
{
    private readonly Dictionary<string, Func<object[], Task<object>>> _mockMethods = new(StringComparer.OrdinalIgnoreCase);

    public void RegisterMockMethod(string? methodName, Func<object[], Task<object>> mockImplementation)
    {
        if (methodName != null)
        {
            _mockMethods[methodName.ToLower()] = mockImplementation;
        }
    }

    public bool IsMockMethodRegistered(string methodName)
    {
        return _mockMethods.ContainsKey(methodName.ToLower());
    }

    public async Task<object> ExecuteMockMethod(string methodName, object[] parameters)
    {
        if (_mockMethods.TryGetValue(methodName.ToLower(), out var method))
        {
            return await method(parameters);
        }

        throw new InvalidOperationException($"No mock method registered for {methodName}");
    }

    public async Task<T> ExecuteAsync<T>(string methodName, object[] parameters)
    {
        var result = await ExecuteMockMethod(methodName, parameters);
        return (T)result;
    }

    public void ClearMockMethods()
    {
        _mockMethods.Clear();
    }
}