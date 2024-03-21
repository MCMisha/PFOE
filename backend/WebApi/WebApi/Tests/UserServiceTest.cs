using NUnit.Framework;
using WebApi.Services;
using WebApi.Models;

namespace WebApi.Tests;

[TestFixture]
public class UserServiceTest
{
    public static IConfiguration InitConfiguration()
    {
        var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddEnvironmentVariables()
            .Build();
        return config;
    }
    private readonly UserService _userService = new(InitConfiguration());

    [Test]
    public void CheckEmail_ExistingEmail_ReturnsTrue()
    {
        const string existingEmail = "existing@example.com";

        var result = _userService.CheckEmail(existingEmail);

        Assert.That(result, Is.True);
    }

    [Test]
    public void CheckEmail_NonExistingEmail_ReturnsFalse()
    {
        const string nonExistingEmail = "nonexisting@example.com";

        var result = _userService.CheckEmail(nonExistingEmail);

        Assert.That(result, Is.False);
    }

    [Test]
    public void AddNewUser_ValidUser_ReturnsUser()
    {
        var newUser = new User
        {
            Login = "newuser",
            FirstName = "newuserfirstname",
            LastName = "newuserlastname",
            Password = "newuserpassword",
            Email = "newuser@example.com",
        };

        var result = _userService.AddNewUser(newUser);

        Assert.That(result, Is.Not.Null);
        Assert.That(newUser.Id.Equals(result.Id));
    }

    [Test]
    public void AddNewUser_DuplicateLogin_ReturnsNull()
    {
        const string existingLogin = "existinglogin";
        _userService.AddNewUser(new User
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

        var result = _userService.AddNewUser(newUser);

        Assert.That(result, Is.Null);
    }

    [Test]
    public void AddNewUser_DuplicateEmail_ReturnsNull()
    {
        const string existingEmail = "existingemail";
        _userService.AddNewUser(new User
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

        var result = _userService.AddNewUser(newUser);

        Assert.That(result, Is.Null);
    }

    [Test]
    public void Login_ValidUser_ReturnsTrue()
    {
        const string login = "good";
        const string password = "good";
        User? GetByLoginFunc() => new() { Login = login, Password = password };
        var service = new UserService(InitConfiguration())
        {
            GetByLoginFunc = GetByLoginFunc
        };

        var result = service.Login(login, password);

        Assert.That(result, Is.True);
    }

    [Test]
    public void Login_InvalidLogin_ReturnsFalse()
    {
        const string login = "bad";
        const string password = "bad";
        User? GetByLoginFunc() => null;
        var service = new UserService(InitConfiguration())
        {
            GetByLoginFunc = GetByLoginFunc
        };

        var result = service.Login(login, password);

        Assert.That(result, Is.False);
    }

    [Test]
    public void Login_InvalidPassword_ReturnsFalse()
    {
        const string login = "good";
        const string password = "bad";
        User? GetByLoginFunc() => new() { Login = login, Password = "good" };
        var service = new UserService(InitConfiguration())
        {
            GetByLoginFunc = GetByLoginFunc
        };

        var result = service.Login(login, password);

        Assert.That(result, Is.False);
    }
}
