using WebApi.Repositories;

namespace WebApi.Services;

public class UserService
{
    private readonly UserRepository _userRepository;

    public UserService(UserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public bool Login(string userName, string password)
    {
        var user = _userRepository.GetUserByUserName(userName);
        return user != null && user.Password == password;
    }
}