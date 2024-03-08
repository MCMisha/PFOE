using WebApi.Services;
using WebApi.Models;
using NUnit.Framework;
using Moq;
using WebApi.Repositories;

namespace WebApi.Tests;

[TestFixture]
public class UserServiceTests
{
    private UserService MakeService()
    {
        var mockRepository = new Mock<UserRepository>();
        return new UserService(mockRepository.Object);
    }

    [Test]
    public void Login_ValidCredentials_ReturnsTrue()
    {
        var service = MakeService();

        var result = service.Login("good", "good");

        Assert.That(result, Is.True);
    }

    [TestCase("bad", "bad")]
    [TestCase("good", "bad")]
    [TestCase("bad", "good")]
    public void Login_InvalidCredentials_ReturnsFalse(string username, string password)
    {
        var service = MakeService();

        var result = service.Login(username, password);

        Assert.That(result, Is.False);
    }
}