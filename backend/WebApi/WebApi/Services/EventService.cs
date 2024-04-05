using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Repositories;

namespace WebApi.Services;

public class EventService
{
    private readonly EventRepository _eventRepository;

    public EventService(IConfiguration configuration)
    {
        _eventRepository = new EventRepository(configuration);
    }

    public List<Event> GetAll()
    {
        return _eventRepository.GetAll();
    }

    public Event? Get(int id)
    {
        return _eventRepository.GetById(id);
    }

    public void Add(Event @event)
    {
        _eventRepository.Add(@event);
    }

    public void Update(Event @event)
    {
        if (_eventRepository.GetById(@event.Id) != null)
        {
            _eventRepository.Update(@event);
        }
    }

    public void Delete(int id)
    {
        var @event = _eventRepository.GetById(id);

        if (@event != null)
        {
            _eventRepository.Delete(@event);
        }
    }

    public ActionResult<List<Event>> GetNewest()
    {
        return _eventRepository.GetNewest();
    }

    public ActionResult<List<Event>> GetMostPopular()
    {
        return _eventRepository.GetMostPopular();
    }
}