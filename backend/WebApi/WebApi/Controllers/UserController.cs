using Microsoft.AspNetCore.Mvc;
using System.Web;
using System.Globalization;
using WebApi.Enums;
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
    private readonly EmailService _emailService;
    public Func<bool>? UserChecker { get; init; } //właściwośc dodana na potrzeby testów jednostkowych


    public UserController(ILogger<UserController> logger, IConfiguration configuration)
    {
        _logger = logger;
        _userService = new UserService(configuration);
        _hashService = new HashService();
        _emailService = new EmailService(configuration);
    }

    [HttpPost("login")]
    public IActionResult Login(string login, string password)
    {
        string decodedLogin = HttpUtility.UrlDecode(login);
        string decodedPassword = HttpUtility.UrlDecode(password);
        var user = _userService.GetByLogin(decodedLogin);
        if (user == null)
        {
            return NotFound();
        }

        var checkLoginAttempts = _userService.CheckLoginAttempts(user.Id);
        if (checkLoginAttempts != null)
        {
            if (checkLoginAttempts.FailedLoginAttempts == 3)
            {
                return NotFound();
            }
        }
        
        if (CheckUserFunc(decodedLogin, decodedPassword))
        {
            _userService.ResetLoginAttempts(user.Id);
            return Ok();
        }
        _userService.IncrementLoginAttempts(user.Id);
        
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
            TextInfo textInfo = new CultureInfo("pl-PL", false).TextInfo;
            _emailService.SendEmailByType(user.Email, string.Join(' ', user.FirstName, user.LastName), textInfo.ToTitleCase(nameof(EmailType.REGISTRATION).ToLower()).Replace("_", ""), user.Login);
            return Ok(newUser);
        }

        return BadRequest();
    }

    [HttpGet("isLogged")]
    public IActionResult IsLogged(string login)
    {
        var user = _userService.GetByLogin(login);
        
        if (user == null)
        {
            return Ok(false);
        }

        var checkLoginAttempts = _userService.CheckLoginAttempts(user.Id);
        if (checkLoginAttempts == null)
        {
            return Ok(false);
        }
        if (checkLoginAttempts.FailedLoginAttempts == 3)
        {
            return Ok(false);
        }
        TimeSpan difference = DateTime.Now - checkLoginAttempts.LastLoginTime;
        
        if (difference.Hours >= 3)
        {
            return Ok(false);
        }

        return Ok(true);
    }
    
    private IEnumerable<User> GetAllUsers()
    {
        return _userService.GetAllUsers();
    }

}