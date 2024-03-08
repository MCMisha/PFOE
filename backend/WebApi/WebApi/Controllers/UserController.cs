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

    public UserController(IUserService userService, HashService hashService)
    {
        _userService = userService;
        _hashService = hashService;
    }

    [HttpPost("login")]
    public IActionResult Login(string login, string password)
    {
        User? loggedUser = _userService.Login(login, _hashService.GetSha256Hash(password));

        if (loggedUser != null)
        {
            return Ok(loggedUser);
        }

        return Unauthorized(loggedUser);
    }
}