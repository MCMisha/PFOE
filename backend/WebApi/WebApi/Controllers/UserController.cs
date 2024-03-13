using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : Controller
{
    private readonly UserService _userService;
    private readonly HashService _hashService;
    public Func<User?>? LoginChecker { get; set; } //właściwośc dodana na potrzeby testów jednostkowych

    public UserController(UserService userService, HashService hashService)
    {
        _userService = userService;
        _hashService = hashService;
    }

    [HttpPost("login")]
    public IActionResult Login(string login, string password)
    {
        User? loggedUser = CheckLogin(login, password);

        if (loggedUser != null)
        {
            return Ok(loggedUser);
        }

        return Unauthorized(loggedUser);
    }

    private User? CheckLogin(string login, string password)
    {
        if(LoginChecker != null)
        {
            return LoginChecker();
        }

        return _userService.Login(login, _hashService.GetSha256Hash(password));
    }
}