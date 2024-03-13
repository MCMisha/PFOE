using WebApi.Contexts;
using WebApi.Models;

namespace WebApi.Repositories;

public class UserRepository
{
    private readonly AppDbContext _appDbContext;

    public UserRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public IEnumerable<User> GetAllUsers()
    {
        return _appDbContext.Users;
    }

    public User? GetByLogin(string login)
    {
        return _appDbContext.Users.FirstOrDefault(user => user.Login == login);
    }
}