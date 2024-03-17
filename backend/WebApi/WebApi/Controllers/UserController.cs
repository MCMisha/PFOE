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
    public Func<bool>? UserChecker { get; init; } //właściwośc dodana na potrzeby testów jednostkowych


    public UserController(ILogger<UserController> logger, IConfiguration configuration)
    {
        _logger = logger;
        _userService = new UserService(configuration);
        _hashService = new HashService();
    }

    [HttpPost("login")]
    public IActionResult Login(string login, string password)
    {
        if (CheckUserFunc(login, password))
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

    [HttpGet("checkLogin")]
    public IActionResult CheckLogin(string login)
    {
        return Ok(_userService.CheckLogin(login));
    }

    private bool CheckUserFunc(string login, string password)
    {
        if (UserChecker != null)
        {
            return UserChecker();
        }

        return _userService.Login(login, _hashService.GetSha256Hash(password));
    }

    [HttpPost("register")]
    public IActionResult AddNewUser(User user)
    {
        user.Password = _hashService.GetSha256Hash(user.Password);
        User? newUser = _userService.AddNewUser(user);
        if (newUser != null)
        {
            return Ok(newUser);
        }

        return BadRequest();
    }
}