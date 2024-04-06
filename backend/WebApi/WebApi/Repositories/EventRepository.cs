using Microsoft.AspNetCore.Mvc;
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

    public List<Event> GetNewest()
    {
        return _appDbContext.Events.OrderByDescending(x => x.CreationDate).Take(5).ToList();
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

    public List<Event> Search(string query)
    {
        return _appDbContext.Events.FromSqlInterpolated(
                $"SELECT id, name, location, organizer, visits_number, creation_date, category, date, participant_number FROM pfoe.event WHERE event.document_idx_col @@ to_tsquery('english', {query})")
            .ToList();
        return _appDbContext.Events.FromSqlInterpolated(
                $"SELECT id, name, location, organizer, visits_number, creation_date, category, date, participant_number FROM pfoe.event WHERE event.document_idx_col @@ to_tsquery('english', {query})")
            .ToList();
    }

    public ActionResult<List<Event>> GetMostPopular()
    {
        return _appDbContext.Events.OrderByDescending(e => e.VisitsNumber).Take(5).ToList();
    }
}