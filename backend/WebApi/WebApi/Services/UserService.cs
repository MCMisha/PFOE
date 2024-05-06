using WebApi.Models;
using WebApi.Repositories;

namespace WebApi.Services;

public class UserService
{
    private readonly UserRepository _userRepository;
    private readonly FailedLoginRepository _failedLoginRepository;
    public Func<User?>? GetByLoginFunc { get; init; } //właściwość dodana na potrzeby testów jednostkowych

    public UserService(IConfiguration configuration)
    {
        _userRepository = new UserRepository(configuration);
        _failedLoginRepository = new FailedLoginRepository(configuration);
    }

    public bool Login(string login, string password)
    {
        var user = GetByLogin(login);

        if (user == null)
        {
            return false;
        }

        var failedLogin = _failedLoginRepository.FindLoginAttemptsByUserId(user.Id);
        if (failedLogin == null && user.Password == password)
        {
            _failedLoginRepository.AddLastLoginTime(user.Id);
            return true;
        }

        if (user.Password != password)
        {
            _failedLoginRepository.IncrementLoginAttempts(user.Id);
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

    public User? GetByLogin(string login)
    {
        if (GetByLoginFunc != null)
        {
            return GetByLoginFunc();
        }

        return _userRepository.GetByLogin(login);
    }

    public User? GetById(int userId)
    {
        return _userRepository.GetById(userId);
    }

    public IEnumerable<User> GetAllUsers()
    {
        return _userRepository.GetAllUsers();
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

    public FailedLogin? CheckLoginAttempts(int userId)
    {
        return _failedLoginRepository.FindLoginAttemptsByUserId(userId);
    }

    public void IncrementLoginAttempts(int userId)
    {
        _failedLoginRepository.IncrementLoginAttempts(userId);
    }

    public void ResetLoginAttempts(int userId)
    {
        _failedLoginRepository.ResetLoginAttempts(userId);
    }

    public void DeleteLoginAttempts(int userId)
    {
        _failedLoginRepository.DeleteLoginAttempts(userId);
    }


    public void AddParticipant(int userId, int eventId)
    {
        _userRepository.AddParticipant(userId, eventId);
    }
    
}