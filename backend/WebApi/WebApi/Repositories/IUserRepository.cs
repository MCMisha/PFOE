using WebApi.Models;

namespace WebApi.Repositories;

public interface IUserRepository
{
    public IEnumerable<User> GetAllUsers();
    public User? GetByLogin(string login);
}