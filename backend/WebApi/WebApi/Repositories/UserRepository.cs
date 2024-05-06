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

    public User? AddNewUser(User user)
    {
        var result = _appDbContext.Users.Add(user);
        _appDbContext.SaveChanges();
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

    public User? GetById(int id)
    {
        return _appDbContext.Users.FirstOrDefault(user => user.Id == id);
    }

    public bool CheckEmail(string email)
    {
        return _appDbContext.Users.FirstOrDefault(user => user.Email == email) != null;
    }

    public void AddParticipant(int userId, int eventId)
    {
        Participant newParticipant = new Participant {
            UserId = userId, EventId = eventId
        };

        _appDbContext.Participants.Add(newParticipant);
        _appDbContext.SaveChanges();
    }

}