using RoMock.Example.App.Models;
using RoMock.Library.Interfaces;

namespace RoMock.Example.App.Repositories.UserRepository;

public interface IUserRepository : IMockable
{
    public Task<IEnumerable<UserModel>?> GetUsersAsync();
}