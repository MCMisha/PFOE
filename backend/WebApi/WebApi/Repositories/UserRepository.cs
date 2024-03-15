using WebApi.Contexts;
using WebApi.Models;

namespace WebApi.Repositories;

public class UserRepository
{
    private readonly AppDbContext _appDbContext;

    public UserRepository(IConfiguration configuration)
    {
        _appDbContext = new AppDbContext(configuration);
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