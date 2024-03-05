using WebApi.Models;
using WebApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly UserService _userService;

    public UserController(UserService userService)
    {
        _userService = userService;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        if (_userService.Login(request.UserName, request.Password))
        {
            return Ok("Login successful.");
        }
        return Unauthorized("Invalid username or password.");
    }
}