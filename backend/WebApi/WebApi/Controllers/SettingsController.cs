using Microsoft.AspNetCore.Mvc;
using WebApi.Services;
using WebApi.Models;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class SettingsController : Controller
{
    private readonly SettingsService _settingsService;
    private ILogger<SettingsController> _logger;


    public SettingsController(ILogger<SettingsController> logger, IConfiguration configuration)
    {
        _logger = logger;
        _settingsService = new SettingsService(configuration);
    }

    [HttpGet("{userId:int}")]
    public IActionResult Get(int userId)
    {
        var settings = _settingsService.Get(userId);

        if (settings == null)
        {
            return NotFound();
        }

        return Ok(settings);
    }

    [HttpPost("new")]
    public IActionResult Create(Setting settings)
    {
        _settingsService.Add(settings);
        return CreatedAtAction(nameof(Get), new { userId = settings.UserId }, settings);
    }


    [HttpPut("edit")]
    public IActionResult Update(Setting settings)
    {
        var existingSettings = _settingsService.Get(settings.UserId);

        if (existingSettings is null)
        {
            return NotFound();
        }

        _settingsService.Update(settings);

        return Ok();
    }
}