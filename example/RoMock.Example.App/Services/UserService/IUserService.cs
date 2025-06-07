using RoMock.Example.App.Models;

namespace RoMock.Example.App.Services.UserService;

public interface IUserService
{
    public Task<IEnumerable<UserModel>?> GetUsersAsync();
}