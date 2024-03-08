using Moq;
using NUnit.Framework;
using WebApi.Models;
using WebApi.Repositories;
using WebApi.Services;

namespace  WebApi.Tests;

[TestFixture]
public class UserServiceTests
{
    private UserService _service;
    private Mock<IUserRepository> _userRepositoryMock;

    [SetUp]
    public void Setup()
    {
        _userRepositoryMock = new Mock<IUserRepository>(MockBehavior.Strict);
        _service = new UserService(_userRepositoryMock.Object);
    }

    [Test]
    public void Login_ValidUser_ReturnsUser()
    {
        string login = "good";
        string password = "good";
        _userRepositoryMock.Setup(x => x.GetByLogin(It.IsAny<string>())).Returns(new User { Login = login, Password = password });

        var result = _service.Login(login, password);

        Assert.That(result, Is.Not.Null);
    }

    [Test]
    public void Login_InvalidLogin_ReturnsNull()
    {
        string login = "bad";
        string password = "bad";
        _userRepositoryMock.Setup(x => x.GetByLogin(It.IsAny<string>())).Returns(null as User);

        var result = _service.Login(login, password);

        Assert.That(result, Is.Null);
    }

    [Test]
    public void Login_InvalidPassword_ReturnsNull()
    {
        string login = "good";
        string password = "bad";
        _userRepositoryMock.Setup(x => x.GetByLogin(It.IsAny<string>())).Returns(new User { Login = login, Password = "good" });

        var result = _service.Login(login, password);

        Assert.That(result, Is.Null);
    }
}

