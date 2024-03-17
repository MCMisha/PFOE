using WebApi.Contexts;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Repositories;

public class UserRepository
{
    private readonly AppDbContext _appDbContext;
    private readonly EmailService _emailService;

    public UserRepository(IConfiguration configuration)
    {
        _appDbContext = new AppDbContext(configuration);
        _emailService = new EmailService(configuration);
    }

    public User? AddNewUser(User user)
    {
        var result = _appDbContext.Users.Add(user);
        _appDbContext.SaveChanges();
        _emailService.SendEmailByType(user.Email, user.FirstName+" "+user.LastName, "REGISTRATION", user.Login);
        return result.Entity;
    }

    public IEnumerable<User> GetAllUsers()
    {
        return _appDbContext.Users;
    }

    public User? GetByLogin(string login)
    {
        return _appDbContext.Users.FirstOrDefault(user => user.Login == login);
    }

    public bool CheckEmail(string email)
    {
        return _appDbContext.Users.FirstOrDefault(user => user.Email == email) != null;
    }
}