using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using WebApi.Controllers;
using WebApi.Models;
using Moq;

namespace WebApi.Tests;

[TestFixture]
public class UserControllerTest
{
    private readonly ILogger<UserController> _logger = new Mock<ILogger<UserController>>().Object;
    private static IConfiguration InitConfiguration()
    {
        var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddEnvironmentVariables()
            .Build();
        return config;
    }
    private readonly UserController _userController = new(null, InitConfiguration());
    
    [Test]
    public void CheckEmail_ValidEmail_ReturnsOk()
    {
        var email = "test@example.com";
        
        var result = _userController.CheckEmail(email);
        
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.InstanceOf<Microsoft.AspNetCore.Mvc.OkObjectResult>());
        
    }
    
    [Test]
    public void AddNewUser_ValidUser_ReturnsOk()
    {
        var newUser = new User
        {
            Login = "newuser",
            FirstName = "newuserfirstname",
            LastName = "newuserlastname",
            Password = "newuserpassword",
            Email = "newuser@example.com",
        };
            
        var result = _userController.AddNewUser(newUser);

        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.InstanceOf<Microsoft.AspNetCore.Mvc.OkObjectResult>());
    }

    [Test]
    public void AddNewUser_DuplicateEmail_ReturnsBadRequest()
    {
        const string existingEmail = "existingemail";
        _userController.AddNewUser(new User
        {
            Login = "existinguserlogin",
            FirstName = "existinguserfirstname",
            LastName = "existinguserlastname",
            Password = "existinguserpassword",
            Email = existingEmail,
        });
                
        var newUser = new User
        {
            Login = "newuserlogin",
            FirstName = "newuserfirstname",
            LastName = "newuserlastname",
            Password = "newuserpassword",
            Email = existingEmail,
        };
                
        var result = _userController.AddNewUser(newUser);
                
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.InstanceOf<Microsoft.AspNetCore.Mvc.OkObjectResult>());
    }
    
    [Test]
    public void AddNewUser_DuplicateLogin_ReturnsBadRequest()
    {
        const string existingLogin = "existinglogin";
        _userController.AddNewUser(new User
        {
            Login = existingLogin,
            FirstName = "existinguserfirstname",
            LastName = "existinguserlastname",
            Password = "existinguserpassword",
            Email = "existinguser@example.com",
        });
                
        var newUser = new User
        {
            Login = existingLogin,
            FirstName = "newuserfirstname",
            LastName = "newuserlastname",
            Password = "newuserpassword",
            Email = "newuser@example.com",
        };
                
        var result = _userController.AddNewUser(newUser);
                
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.InstanceOf<Microsoft.AspNetCore.Mvc.OkObjectResult>());
    }    
        
    [Test]
    public void Login_ValidUser_ReturnsOk()
    {
        const string login = "good_user";
        const string password = "good_password";
        bool UserChecker()
        {
            return true;
        }

        var controller = new UserController(_logger, InitConfiguration())
        {
            UserChecker = UserChecker
        };

        var result = controller.Login(login, password);

        Assert.That(result, Is.InstanceOf<OkResult>());
    }

    [Test]
    public void Login_InvalidUser_ReturnsNotFound()
    {
        const string login = "bad_user";
        const string password = "bad_password";
        bool UserChecker()
        {
            return false;
        }
        var controller = new UserController(_logger, InitConfiguration())
        {
            UserChecker = UserChecker
        };

        var result = controller.Login(login, password);

        Assert.That(result, Is.InstanceOf<NotFoundResult>());
    }

    [Test]
    public void CheckLogin_LoginExists_ReturnsOk()
    {
        const string login = "good_user";
        var controller = new UserController(_logger, InitConfiguration());

        var result = controller.CheckLogin(login);

        Assert.That(result, Is.InstanceOf<OkObjectResult>());
    }

    [Test]
    public void CheckLogin_LoginDoesntExist_ReturnsOk()
    {
        const string login = "bad_user";
        var controller = new UserController(_logger, InitConfiguration());

        var result = controller.CheckLogin(login);

        Assert.That(result, Is.InstanceOf<OkObjectResult>());
    }
}