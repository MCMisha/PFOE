using Moq;
using NUnit.Framework;
using WebApi.Contexts;
using WebApi.Models;
using WebApi.Repositories;
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
    public void Login_ValidUser_ReturnsUser()
    {
        string login = "good";
        string password = "good";
        Func<User?> getByLoginFunc = () => { return new User { Login = login, Password = password }; };
        var service = new UserService(new UserRepository(new AppDbContext(InitConfiguration())));
        service.GetByLoginFunc = getByLoginFunc;

        var result = service.Login(login, password);

        Assert.That(result, Is.Not.Null);
    }

    [Test]
    public void Login_InvalidLogin_ReturnsNull()
    {
        string login = "bad";
        string password = "bad";
        Func<User?> getByLoginFunc = () => { return null; };
        var service = new UserService(new UserRepository(new AppDbContext(InitConfiguration())));
        service.GetByLoginFunc = getByLoginFunc;

        var result = service.Login(login, password);

        Assert.That(result, Is.Null);
    }

    [Test]
    public void Login_InvalidPassword_ReturnsNull()
    {
        string login = "good";
        string password = "bad";
        Func<User?> getByLoginFunc = () => { return new User { Login = login, Password = "good" }; };
        var service = new UserService(new UserRepository(new AppDbContext(InitConfiguration())));
        service.GetByLoginFunc = getByLoginFunc;

        var result = service.Login(login, password);

        Assert.That(result, Is.Null);
    }
}

