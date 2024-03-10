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
    public UserController(ILogger<UserController> logger, IConfiguration configuration)
    {
        _logger = logger;
        _userService = new UserService(configuration);
        _hashService = new HashService();
    }

    [HttpGet("checkEmail")]
    public IActionResult CheckEmail(string email)
    {
        return Ok(_userService.CheckEmail(email));
    }

    [HttpPost("register")]
    public IActionResult AddNewUser(User user)
    {
        user.Password = _hashService.GetHash(user.Password);
        User? newUser = _userService.AddNewUser(user);
        if (newUser != null)
        {
            return Ok(newUser);
        }

        return BadRequest();
    }
}