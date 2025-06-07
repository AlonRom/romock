using Castle.DynamicProxy;
using RoMock.Library.Interceptors;
using RoMock.Library.Model;
using RoMock.Library.Services;
using RoMock.Library.ViewModels;
using RoMock.Library.Views;

namespace RoMock.Library.Extensions;

public static class MauiAppBuilderExtensions
{
    public static MauiAppBuilder RegisterRoMock(this MauiAppBuilder builder, RoMockConfigurationModel roMockConfigurationModel)
    {
        var services = builder.Services;

        // Register the IRoMockExecutor implementation
        services.AddSingleton<IRoMockExecutor, RoMockExecutor>();

        // Register the MockInterceptor
        services.AddTransient<MockInterceptor>();

        // Add Castle Proxy Generator
        services.AddSingleton<IProxyGenerator, ProxyGenerator>();

        // Register the HttpClient with specific BaseAddress configuration
        services.AddHttpClient<RoMockService>(client =>
        {
            client.BaseAddress = roMockConfigurationModel.BaseAddress;
        });

        var serviceProvider = services.BuildServiceProvider();
        var proxyGenerator = serviceProvider.GetRequiredService<IProxyGenerator>();
        // Register the RoMockService with the required dependencies
        services.AddSingleton<IRoMockService>(sp =>
        {
            var httpClientFactory = sp.GetRequiredService<IHttpClientFactory>();
            var httpClient = httpClientFactory.CreateClient(nameof(RoMockService));
            var roMockExecutor = sp.GetRequiredService<IRoMockExecutor>();
            return new RoMockService(httpClient, roMockExecutor);
        });

        // Call AddProxiedRepositories to register the repositories
        services.AddProxiedRepositories(proxyGenerator);

        // Register the RoMockViewModel and RoMockPage
        services.AddSingleton<RoMockViewModel>();
        services.AddSingleton<RoMockPage>();

        // Register route for RoMockPage
        Routing.RegisterRoute("romock", typeof(RoMockPage));

        return builder;
    }
}