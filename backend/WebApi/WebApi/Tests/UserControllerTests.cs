using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using WebApi.Controllers;
using Moq;
using WebApi.Services;

namespace WebApi.Tests;

[TestFixture]
public class UserControllerTests
{
    private UserController MakeController()
    {
        var mockUserService = new Mock<UserService>();
        return new UserController(mockUserService.Object);
    }

    [Test]
    public void Login_ValidCredentials_ReturnsOk()
    {
        var controller = MakeController();

        var result = controller.Login("user1", "password1");
        var okResult = result as OkObjectResult;

        Assert.That(okResult, Is.Not.Null);
        Assert.That(okResult.StatusCode, Is.EqualTo(200));
    }

    [Test]
    public void Login_InvalidCredentials_ReturnsUnauthorized()
    {
        var controller = MakeController();

        var result = controller.Login("user1", "password2");
        var unauthorizedResult = result as UnauthorizedObjectResult;

        Assert.That(unauthorizedResult, Is.Not.Null);
        Assert.That(unauthorizedResult.StatusCode, Is.EqualTo(401));
    }
}