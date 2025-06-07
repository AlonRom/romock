namespace RoMock.Library.Services;

public interface IRoMockExecutor
{
    void RegisterMockMethod(string? methodName, Func<object[], Task<object>> mockImplementation);
    bool IsMockMethodRegistered(string methodName);
    Task<object> ExecuteMockMethod(string methodName, object[] parameters);
    Task<T> ExecuteAsync<T>(string methodName, object[] parameters);
    void ClearMockMethods();
}