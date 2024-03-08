using WebApi.Models;
using WebApi.Repositories;

namespace WebApi.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
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