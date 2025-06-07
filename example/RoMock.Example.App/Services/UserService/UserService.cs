using RoMock.Example.App.Models;
using RoMock.Example.App.Repositories.UserRepository;

namespace RoMock.Example.App.Services.UserService;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<IEnumerable<UserModel>?> GetUsersAsync()
    {
        return await _userRepository.GetUsersAsync();
    }
}