using System.Reflection;

namespace RoMock.Library.Services;

public interface IRoMockService
{
    void RegisterMockMethod(string? methodName, MethodInfo? method);

    Task<object> ExecuteMockMethod(string? methodName, MethodInfo? method);

    Task<object> DeserializeResponse(HttpResponseMessage response, Type returnType);
}