using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : Controller
{
    private readonly IUserService _userService;
    private readonly HashService _hashService;

    public UserController(IConfiguration configuration)
    {
        _userService = new UserService(configuration);
        _hashService = new HashService();
    }

    public UserController(IUserService userService)
    {
        _userService = userService;
        _hashService = new HashService();
    }

    [HttpPost("login")]
    public IActionResult Login(string login, string password)
    {
        User? loggedUser = _userService.Login(login, _hashService.GetHash(password));

        if (loggedUser != null)
        {
            return Ok(loggedUser);
        }

        return Unauthorized(loggedUser);
    }
}