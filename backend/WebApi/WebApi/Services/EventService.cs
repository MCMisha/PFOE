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
        return _eventRepository.GetAll().ToList();
    }

    public Event? Get(int id)
    {
        return _eventRepository.GetById(id);
    }

    public void Add(Event @event)
    {
        @event.Id = _eventRepository.MaxId() + 1;
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
}