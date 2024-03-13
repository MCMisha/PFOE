using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using WebApi.Controllers;
using WebApi.Contexts;
using WebApi.Models;
using WebApi.Repositories;
using WebApi.Services;

namespace WebApi.Tests;

[TestFixture]
public class UserControllerTests
{
    private static IConfiguration InitConfiguration()
    {
        var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddEnvironmentVariables()
            .Build();
        return config;
    }

    [Test]
    public void Login_ValidUser_ReturnsOk()
    {
        string login = "user";
        string password = "password";
        Func<User?> loginChecker = () => { return new User(); };
        var controller = new UserController(new UserService(new UserRepository(new AppDbContext(InitConfiguration()))), new HashService());
        controller.LoginChecker = loginChecker;

        var result = controller.Login(login, password);

        Assert.That(result, Is.InstanceOf<OkObjectResult>());
    }

    [Test]
    public void Login_InvalidUser_ReturnsUnauthorized()
    {
        string login = "baduser";
        string password = "badpassword";
        Func<User?> loginChecker = () => { return null; };
        var controller = new UserController(new UserService(new UserRepository(new AppDbContext(InitConfiguration()))), new HashService());
        controller.LoginChecker = loginChecker;

        var result = controller.Login(login, password);

        Assert.That(result, Is.InstanceOf<UnauthorizedObjectResult>());
    }
}