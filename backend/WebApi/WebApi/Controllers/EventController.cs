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
    
    [HttpGet("newest")]
    public ActionResult<List<Event>?> GetNewest()
    {
        return _eventService.GetNewest();
    }
    
    [HttpGet("most-popular")]
    public ActionResult<List<Event>?> GetMostPopular()
    {
        return _eventService.GetMostPopular();
    }

    [HttpGet("{id:int}")]
    public IActionResult Get(int id)
    {
        var @event = _eventService.Get(id);

        if (@event == null)
        {
            return NotFound();
        }

        return Ok(@event);
    }

    [HttpPost("new")]
    public IActionResult Create(Event @event)
    {
        _eventService.Add(@event);
        return CreatedAtAction(nameof(Get), new { id = @event.Id }, @event);
    }

    [HttpPut("edit")]
    public IActionResult Update(Event @event)
    {
        var existingEvent = _eventService.Get(@event.Id);

        if (existingEvent is null)
        {
            return NotFound();
        }
        
        _eventService.Update(@event);

        return Ok();
    }

    [HttpDelete("delete/{id:int}")]
    public IActionResult Delete(int id)
    {
        var @event = _eventService.Get(id);

        if (@event is null)
        {
            return NotFound();
        }

        _eventService.Delete(id);

        return Ok();
    }

    [HttpGet("search/{query}")]
    public ActionResult<List<Event>> Search(string query)
    {
        var events = _eventService.Search(query);

        if (events.Count == 0)
        {
            return NotFound();
        }

        return events;
    }
}