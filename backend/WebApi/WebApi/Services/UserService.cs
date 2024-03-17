using WebApi.Models;
using WebApi.Repositories;

namespace WebApi.Services;

public class UserService
{
    private readonly UserRepository _userRepository;
    public Func<User?>? GetByLoginFunc {get; init;} //właściwość dodana na potrzeby testów jednostkowych

    public UserService(IConfiguration configuration)
    {
        _userRepository = new UserRepository(configuration);
    }

    public bool Login(string login, string password)
    {
        var user = GetByLogin(login);

        if (user == null || user.Password != password)
        {
            return false;
        }

        return true;
    }

    public bool CheckLogin(string login)
    {
        return _userRepository.GetByLogin(login) != null;
    }

    public bool CheckEmail(string email)
    {
        return _userRepository.CheckEmail(email);
    }

    private User? GetByLogin(string login)
    {
        if (GetByLoginFunc != null)
        {
            return GetByLoginFunc();
        }

        return _userRepository.GetByLogin(login);
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