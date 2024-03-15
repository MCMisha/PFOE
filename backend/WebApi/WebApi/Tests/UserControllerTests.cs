using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using WebApi.Controllers;
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