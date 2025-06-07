using Castle.DynamicProxy;
using RoMock.Library.Attributes;
using RoMock.Library.Interceptors;
using RoMock.Library.Interfaces;
using RoMock.Library.Services;

namespace RoMock.Library.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddProxiedRepositories(this IServiceCollection services, IProxyGenerator proxyGenerator)
    {
        // Get all currently loaded assemblies
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();

        // Find all interfaces that implement the IMockable marker interface
        var interfaceTypes = assemblies.FindMockableInterfaces(typeof(IMockable));

        foreach (var interfaceType in interfaceTypes)
        {
            // Find classes that implement the interface
            var implementationType = assemblies.FindImplementationType(interfaceType);

            if (implementationType != null)
            {
                // Check if any method in the implementation class has the MockableMethodAttribute
                var methodsWithAttribute = implementationType.GetMethods()
                    .Where(m => m.GetCustomAttributes(typeof(MockableMethodAttribute), false).Any())
                    .ToArray();

                if (methodsWithAttribute.Length > 0)
                {
                    services.AddSingleton(interfaceType, sp =>
                    {
                        var implementationInstance = ActivatorUtilities.CreateInstance(sp, implementationType);
                        var roMockExecutor = sp.GetRequiredService<IRoMockExecutor>();
                        var proxy = proxyGenerator.CreateInterfaceProxyWithTargetInterface(
                            interfaceType, implementationInstance, new MockInterceptor(roMockExecutor));
                        return proxy;
                    });
                }
                else
                {
                    services.AddScoped(interfaceType, implementationType);
                }
            }
        }

        return services;
    }
}
