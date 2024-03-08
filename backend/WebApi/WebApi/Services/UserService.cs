using WebApi.Models;
using WebApi.Repositories;

namespace WebApi.Services;

public class UserService : IUserService
{
    private readonly UserRepository _userRepository;

    public UserService(IConfiguration configuration)
    {
        _userRepository = new UserRepository(configuration);
    }

    public User? Login(string login, string password)
    {
        var user = _userRepository.GetByLogin(login);

        if (user == null)
        {
            return null;
        }

        if (user.Password == password)
        {
            return user;
        }

        return null;
    }
}