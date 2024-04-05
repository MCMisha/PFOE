using WebApi.Contexts;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Repositories;

public class FailedLoginRepository
{
    private readonly AppDbContext _appDbContext;

    public FailedLoginRepository(IConfiguration configuration)
    {
        _appDbContext = new AppDbContext(configuration);
    }

    public FailedLogin? FindLoginAttemptsByUserId(int userId)
    {
        return _appDbContext.FailedLogins.FirstOrDefault(failedLogin => failedLogin.UserId == userId);
    }

    public void IncrementLoginAttempts(int userId)
    {
        var failedLoginToIncrement = _appDbContext.FailedLogins.FirstOrDefault(failedLogin => failedLogin.UserId == userId);
        if (failedLoginToIncrement == null)
        {
            return;
        }
        failedLoginToIncrement.FailedLoginAttempts++;
        failedLoginToIncrement.LastLoginTime = DateTime.Now;
        _appDbContext.FailedLogins.Update(failedLoginToIncrement);
        _appDbContext.SaveChanges();
    }

    public void ResetLoginAttempts(int userId)
    {
        var failedLoginToReset = _appDbContext.FailedLogins.FirstOrDefault(failedLogin => failedLogin.UserId == userId);
        if (failedLoginToReset == null)
        {
            return;
        }
        failedLoginToReset.FailedLoginAttempts = 0;
        failedLoginToReset.LastLoginTime = DateTime.Now;
        _appDbContext.FailedLogins.Update(failedLoginToReset);
        _appDbContext.SaveChanges();
    }

    public void AddLastLoginTime(int userId)
    {
        var newFailedLogin = new FailedLogin { LastLoginTime = DateTime.Now, FailedLoginAttempts = 0, UserId = userId };
        _appDbContext.FailedLogins.Add(newFailedLogin);
        _appDbContext.SaveChanges();
    }
    
    
}