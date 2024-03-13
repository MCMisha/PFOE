using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using WebApi.Controllers;
using WebApi.Contexts;
using WebApi.Models;
using WebApi.Repositories;
using WebApi.Services;
using Moq;

namespace WebApi.Tests;

[TestFixture]
public class UserControllerTests
{
    private ILogger<UserController> _logger = new Mock<ILogger<UserController>>().Object;
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
        var controller = new UserController(_logger, InitConfiguration());
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
        var controller = new UserController(_logger, InitConfiguration());
        controller.LoginChecker = loginChecker;

        var result = controller.Login(login, password);

        Assert.That(result, Is.InstanceOf<UnauthorizedObjectResult>());
    }
}