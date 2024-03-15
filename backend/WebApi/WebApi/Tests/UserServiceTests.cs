using NUnit.Framework;
using WebApi.Models;
using WebApi.Services;

namespace  WebApi.Tests;

[TestFixture]
public class UserServiceTests
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

