using WebApi.Models;

namespace WebApi.Services;

public interface IUserService
{
    public User? Login(string login, string password);
}