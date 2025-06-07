namespace RoMock.Example.App.Services.NavigationService;

public class NavigationService : INavigationService
{
    public Task GoToHome() => Shell.Current.GoToAsync("home");
    public Task GoToRoMock() => Shell.Current.GoToAsync("romock");
    public Task GoToPostsPage() => Shell.Current.GoToAsync("posts");
    public Task GoToCommentsPage() => Shell.Current.GoToAsync("comments");
    public Task GoToToDosPage() => Shell.Current.GoToAsync("todos");
    public Task GoToUsersPage() => Shell.Current.GoToAsync("users");
    public async Task GoToPostPage(string postId)
    {
        var parameters = new Dictionary<string, object> { { "PostId", postId } };
        await Shell.Current.GoToAsync("post", parameters);
    }
}