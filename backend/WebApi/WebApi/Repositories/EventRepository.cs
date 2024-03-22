using Microsoft.EntityFrameworkCore;
using WebApi.Contexts;
using WebApi.Models;

namespace WebApi.Repositories;

public class EventRepository
{
    private readonly AppDbContext _appDbContext;

    public EventRepository(IConfiguration configuration)
    {
        _appDbContext = new AppDbContext(configuration);
    }

    public List<Event> GetAll()
    {
        return _appDbContext.Events.ToList();
    }

    public Event? GetById(int id)
    {
        return _appDbContext.Events.AsNoTracking().FirstOrDefault(e => e.Id == id);
    }

    public void Add(Event @event)
    {
        _appDbContext.Events.Add(@event);
        _appDbContext.SaveChanges();
    }

    public void Update(Event @event)
    {
        _appDbContext.Events.Update(@event);
        _appDbContext.SaveChanges();
    }

    public void Delete(Event @event)
    {
        _appDbContext.Events.Remove(@event);
        _appDbContext.SaveChanges();
    }

    public int MaxId()
    {
        if (!_appDbContext.Events.Any())
        {
            return 0;
        }

        return _appDbContext.Events.Max(e => e.Id);
    }
}