using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using WebApi.Controllers;
using WebApi.Models;
using Moq;

namespace WebApi.Tests;

[TestFixture]
public class UserControllerTests
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

    [Test]
    public void Login_ValidUser_ReturnsOk()
    {
        const string login = "good_user";
        const string password = "good_password";
        User? LoginChecker() => new User();
        var controller = new UserController(_logger, InitConfiguration())
        {
            LoginChecker = LoginChecker
        };

        var result = controller.Login(login, password);

        Assert.That(result, Is.InstanceOf<OkObjectResult>());
    }

    [Test]
    public void Login_InvalidUser_ReturnsBadRequest()
    {
        const string login = "bad_user";
        const string password = "bad_password";
        User? LoginChecker() => null;
        var controller = new UserController(_logger, InitConfiguration())
        {
            LoginChecker = LoginChecker
        };

        var result = controller.Login(login, password);

        Assert.That(result, Is.InstanceOf<BadRequestResult>());
    }
}