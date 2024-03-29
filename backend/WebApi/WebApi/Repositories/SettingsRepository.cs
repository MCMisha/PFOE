using Microsoft.EntityFrameworkCore;
using WebApi.Contexts;
using WebApi.Models;

namespace WebApi.Repositories;

public class SettingsRepository
{
    private readonly AppDbContext _appDbContext;

    public SettingsRepository(IConfiguration configuration)
    {
        _appDbContext = new AppDbContext(configuration);
    }

    public Setting? GetByUserId(int userId)
    {
        return _appDbContext.Settings.AsNoTracking().FirstOrDefault(s => s.UserId == userId);
    }

    public void Add(Setting settings)
    {
        _appDbContext.Settings.Add(settings);
        _appDbContext.SaveChanges();
    }

    public void Update(Setting settings)
    {
        _appDbContext.Settings.Update(settings);
        _appDbContext.SaveChanges();
    }
}