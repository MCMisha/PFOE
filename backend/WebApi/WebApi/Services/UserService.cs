using WebApi.Models;
using WebApi.Repositories;

namespace WebApi.Services;

public class UserService
{
    private readonly UserRepository _userRepository;

    public UserService(IConfiguration configuration)
    {
        _userRepository = new UserRepository(configuration);
    }

    public bool CheckEmail(string email)
    {
        return _userRepository.CheckEmail(email);
    }

    public User? AddNewUser(User user)
    {
        var existingUser = _userRepository.GetByLogin(user.Login);
        if (existingUser != null || _userRepository.CheckEmail(user.Email))
        {
            return null;
        }
        return _userRepository.AddNewUser(user);
    }
}