using Microsoft.Extensions.Logging;
using RoMock.Example.App.Repositories.CommentRepository;
using RoMock.Example.App.Repositories.PostRepository;
using RoMock.Example.App.Repositories.ToDoRepository;
using RoMock.Example.App.Repositories.UserRepository;
using RoMock.Example.App.Services.CommentService;
using RoMock.Example.App.Services.NavigationService;
using RoMock.Example.App.Services.PostService;
using RoMock.Example.App.Services.ToDoService;
using RoMock.Example.App.Services.UserService;
using RoMock.Example.App.ViewModels;
using RoMock.Example.App.Views;
using RoMock.Library.Extensions;
using RoMock.Library.Model;

namespace RoMock.Example.App;

public static class MauiProgram
{
    public static string JsonPlaceholderClient = "jsonplaceholderClient";
    public static string BaseAddress = "https://jsonplaceholder.typicode.com/";
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            })
            .RegisterRepositories()
            .RegisterServices()
            .RegisterViewModels()
            .RegisterViews()
            .RegisterRoMock(new RoMockConfigurationModel
            {
                BaseAddress = new Uri("https://romock.free.beeceptor.com/")
            });

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
    private static MauiAppBuilder RegisterServices(this MauiAppBuilder builder)
    {
        builder.Services.AddSingleton<INavigationService, NavigationService>();
        builder.Services.AddSingleton<IPostService, PostService>();
        builder.Services.AddSingleton<ICommentService, CommentService>();
        builder.Services.AddSingleton<IToDoService, ToDoService>();
        builder.Services.AddSingleton<IUserService, UserService>();
        return builder;
    }

    private static MauiAppBuilder RegisterRepositories(this MauiAppBuilder builder)
    {
        builder.Services.AddHttpClient<IPostRepository>(JsonPlaceholderClient, client =>
        {
            client.BaseAddress = new Uri(BaseAddress);
            client.DefaultRequestHeaders.Add("Accept", "application/json");
        });

        builder.Services.AddTransient<IPostRepository, PostRepository>();
        builder.Services.AddTransient<ICommentRepository, CommentRepository>();
        builder.Services.AddTransient<IToDoRepository, ToDoRepository>();
        builder.Services.AddTransient<IUserRepository, UserRepository>();
        return builder;
    }

    private static MauiAppBuilder RegisterViewModels(this MauiAppBuilder builder)
    {
        builder.Services.AddSingleton<LoginViewModel>();
        builder.Services.AddSingleton<HomeViewModel>();

        builder.Services.AddTransient<PostsViewModel>();
        builder.Services.AddTransient<CommentsViewModel>();
        builder.Services.AddTransient<ToDosViewModel>();
        builder.Services.AddTransient<UsersViewModel>();
        builder.Services.AddTransient<PostViewModel>();
        return builder;
    }

    private static MauiAppBuilder RegisterViews(this MauiAppBuilder builder)
    {
        builder.Services.AddSingleton<LoginPage>();
        builder.Services.AddSingleton<HomePage>();

        builder.Services.AddTransient<PostsPage>();
        builder.Services.AddTransient<CommentsPage>();
        builder.Services.AddTransient<ToDosPage>();
        builder.Services.AddTransient<UsersPage>();
        builder.Services.AddTransient<PostPage>();
        return builder;
    }
}