namespace RoMock.Example.App.Services.NavigationService;

public interface INavigationService
{
    public Task GoToHome();
    public Task GoToRoMock();
    public Task GoToPostsPage();
    public Task GoToCommentsPage();
    public Task GoToToDosPage();
    public Task GoToUsersPage();
    public Task GoToPostPage(string postId);
}