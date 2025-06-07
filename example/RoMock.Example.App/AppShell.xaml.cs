using RoMock.Example.App.Views;

namespace RoMock.Example.App;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        RegisterRoutes();
    }

    private static void RegisterRoutes()
    {
        Routing.RegisterRoute("home", typeof(HomePage));
        Routing.RegisterRoute("posts", typeof(PostsPage));
        Routing.RegisterRoute("comments", typeof(CommentsPage));
        Routing.RegisterRoute("todos", typeof(ToDosPage));
        Routing.RegisterRoute("users", typeof(UsersPage));
        Routing.RegisterRoute("post", typeof(PostPage));
    }
}