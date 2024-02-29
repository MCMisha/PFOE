using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebApi.Contexts;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : Controller
{
    private AppDbContext _appDbContext;
    public UserController(IConfiguration configuration)
    {
        _appDbContext = new AppDbContext(configuration);
    }
    // GET
    [HttpGet("getUsers")]
    public IActionResult GetUsers()
    {
        return Ok(_appDbContext.Users.ToList());
    }
}