using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Enums;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class EventController : Controller
{
    private ILogger<EventController> _logger;
    private readonly EventService _eventService;
    private readonly UserService _userService;

    public EventController(ILogger<EventController> logger, IConfiguration configuration)
    {
        _logger = logger;
        _eventService = new EventService(configuration);
        _userService = new UserService(configuration);
    }

    [HttpGet]
    public ActionResult<List<Event>?> GetAll()
    {
        return _eventService.GetAll();
    }

    [HttpGet("organizer/{organizerId}")]
    public ActionResult<List<Event>> GetByOrganizerId(int organizerId)
    {
        return _eventService.GetByOrganizer(organizerId);
    }

    [HttpGet("newest")]
    public ActionResult<List<Event>> GetNewest()
    {
        return _eventService.GetNewest();
    }

    [HttpGet("most-popular")]
    public ActionResult<List<Event>> GetMostPopular()
    {
        return _eventService.GetMostPopular();
    }

    [HttpGet("{id:int}")]
    public IActionResult GetEvent(int id)
    {
        var @event = _eventService.GetEventAndIncreaseVisits(id);

        if (@event == null)
        {
            return NotFound();
        }

        return Ok(@event);
    }

    [HttpGet("edit/{id:int}")]
    public IActionResult Get(int id)
    {
        var @event = _eventService.GetEvent(id);

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
        var existingEvent = _eventService.GetEvent(@event.Id);

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
        var @event = _eventService.GetEvent(id);

        if (@event is null)
        {
            return NotFound();
        }

        _eventService.Delete(id);

        return Ok();
    }

    [HttpGet("getParticipantNumber/{id:int}")]
    public IActionResult GetParticipantNumber(int id)
    {
        var participantNumber = _eventService.GetParticipantNumber(id);
        if (participantNumber == null)
        {
            return NotFound(id);
        }
        return Ok(_eventService.GetParticipantNumber(id));
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

    [HttpPost("addParticipant")]
    public IActionResult AddParticipant(int userId, int eventId)
    {
        _eventService.AddParticipant(userId, eventId);
        var _event = _eventService.GetEvent(eventId);
        var user = _userService.GetById(userId);
        if (_event == null)
        {
            return NotFound("Wydarzenie nie zostało znalezione");
        }

        if (user == null)
        {
            return BadRequest("Użytkownik nie został znaleniony");
        }
        return Ok();
    }

    [HttpGet("isUserSignedUpForEvent")]
    public IActionResult IsUserSignedUpForEvent(int userId, int eventId)
    {
        var isSignedUp = _eventService.IsUserSignedUpForEvent(userId, eventId);

        return Ok(isSignedUp);
    }
}
