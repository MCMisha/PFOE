using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : Controller
{
    private readonly ILogger<UserController> _logger;
    private readonly UserService _userService;
    private readonly HashService _hashService;
    public Func<bool>? LoginChecker { get; init; } //właściwośc dodana na potrzeby testów jednostkowych

    public UserController(ILogger<UserController> logger, IConfiguration configuration)
    {
        _logger = logger;
        _userService = new UserService(configuration);
        _hashService = new HashService();
    }

    [HttpPost("login")]
    public IActionResult Login(string login, string password)
    {
        if (CheckLoginFunc(login, password))
        {
            return Ok();
        }

        return NotFound();
    }

    [HttpGet("checkEmail")]
    public IActionResult CheckEmail(string email)
    {
        return Ok(_userService.CheckEmail(email));
    }

    private bool CheckLoginFunc(string login, string password)
    {
        if(LoginChecker != null)
        {
            return LoginChecker();
        }

        return _userService.Login(login, _hashService.GetSha256Hash(password));
    }
}