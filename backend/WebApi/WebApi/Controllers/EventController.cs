using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class EventController : Controller
{
    private ILogger<EventController> _logger;
    private readonly EventService _eventService;

    public EventController(ILogger<EventController> logger, IConfiguration configuration)
    {
        _logger = logger;
        _eventService = new EventService(configuration);
    }

    [HttpGet]
    public ActionResult<List<Event>?> GetAll()
    {
        return _eventService.GetAll();
    }

    [HttpGet("{id:int}")]
    public ActionResult<Event?> Get(int id)
    {
        var @event = _eventService.Get(id);

        if (@event == null)
        {
            return NotFound();
        }

        return @event;
    }

    [HttpPost]
    public IActionResult Create(Event @event)
    {
        _eventService.Add(@event);
        return CreatedAtAction(nameof(Get), new { id = @event.Id }, @event);
    }

    [HttpPut("{id:int}")]
    public IActionResult Update(int id, Event @event)
    {
        if(id != @event.Id)
        {
            return BadRequest();
        }

        var existingEvent = _eventService.Get(id);

        if (existingEvent is null)
        {
            return NotFound();
        }

        _eventService.Update(@event);

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        var @event = _eventService.Get(id);

        if (@event is null)
        {
            return NotFound();
        }

        _eventService.Delete(id);

        return NoContent();
    }
}