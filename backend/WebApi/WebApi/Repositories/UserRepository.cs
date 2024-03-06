using WebApi.Contexts;
using WebApi.Models;

namespace WebApi.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public User GetUserByUserName(string userName)
    {
        return _context.Users.FirstOrDefault(u => u.Login == userName);
    }

    public User GetUserById(int id)
    {
        return _context.Users.FirstOrDefault(u => u.Id == id);
    }
}