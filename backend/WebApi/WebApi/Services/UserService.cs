using WebApi.Models;
using WebApi.Repositories;

namespace WebApi.Services;

public class UserService
{
    private readonly UserRepository _userRepository;
    public Func<User?>? GetByLoginFunc {get; set;} //właściwośc dodana na potrzeby testów jednostkowych

    public UserService(IConfiguration configuration)
    {
        _userRepository = new UserRepository(configuration);
    }

    public User? Login(string login, string password)
    {
        var user = GetByLogin(login);

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

    private User? GetByLogin(string login)
    {
        if (GetByLoginFunc != null)
        {
            return GetByLoginFunc();
        }

        return _userRepository.GetByLogin(login);
    }
}