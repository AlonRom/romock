using Castle.DynamicProxy;
using RoMock.Library.Services;

namespace RoMock.Library.Interceptors;

public class MockInterceptor : IInterceptor
{
    private readonly IRoMockExecutor _roMockExecutor;

    public MockInterceptor(IRoMockExecutor roMockExecutor)
    {
        _roMockExecutor = roMockExecutor;
    }

    public void Intercept(IInvocation invocation)
    {
        var methodName = invocation.Method.Name;
        // Asynchronous method handling
        if (invocation.Method.ReturnType.IsGenericType && invocation.Method.ReturnType.GetGenericTypeDefinition() == typeof(Task<>))
        {
            if (_roMockExecutor.IsMockMethodRegistered(methodName))
            {
                var resultType = invocation.Method.ReturnType.GetGenericArguments()[0];
                var executeAsyncMethod = typeof(RoMockExecutor).GetMethod(nameof(RoMockExecutor.ExecuteAsync))?.MakeGenericMethod(resultType);

                // Call ExecuteAsync and return the Task
                invocation.ReturnValue = executeAsyncMethod?.Invoke(_roMockExecutor, [methodName, invocation.Arguments
                ]);
            }
            else
            {
                // Proceed with the original implementation if the mock method is not found
                invocation.Proceed();
            }
        }
        else
        {
            throw new InvalidOperationException("Only asynchronous methods are supported.");
        }
    }
}
