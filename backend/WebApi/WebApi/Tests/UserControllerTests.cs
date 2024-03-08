using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using WebApi.Controllers;
using Moq;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Tests;

[TestFixture]
public class UserControllerTests
{
    private UserController _controller;
    private Mock<IUserService> _userServiceMock;

    [SetUp]
    public void Setup()
    {
        _userServiceMock = new Mock<IUserService>();
        _controller = new UserController(_userServiceMock.Object);
    }

    [Test]
    public void Login_ValidUser_ReturnsOk()
    {
        string login = "user";
        string password = "password";
        _userServiceMock.Setup(x => x.Login(It.IsAny<string>(), It.IsAny<string>()) ).Returns(new User());

        var result = _controller.Login(login, password);

        Assert.That(result, Is.InstanceOf<OkObjectResult>());
    }

    [Test]
    public void Login_InvalidUser_ReturnsUnauthorized()
    {
        string login = "baduser";
        string password = "badpassword";
        _userServiceMock.Setup(x => x.Login(It.IsAny<string>(), It.IsAny<string>()) ).Returns(null as User);

        var result = _controller.Login(login, password);

        Assert.That(result, Is.InstanceOf<UnauthorizedObjectResult>());
    }
}