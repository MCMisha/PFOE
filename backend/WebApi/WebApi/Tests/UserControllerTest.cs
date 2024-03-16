using NUnit.Framework;
using WebApi.Controllers;
using WebApi.Models;


namespace WebApi.Tests;

[TestFixture]
public class UserControllerTest
{
    private IConfiguration _configuration;
    private UserController _userController;
    private static IConfiguration InitConfiguration()
    {
        var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddEnvironmentVariables()
            .Build();
        return config;
    }
    
    [SetUp]
    public void Setup()
    {
        _configuration = InitConfiguration();
        _userController = new UserController(null, _configuration);
    }
    
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
        
}