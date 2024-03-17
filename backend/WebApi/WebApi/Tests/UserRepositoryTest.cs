using WebApi.Repositories;
using NUnit.Framework;
using WebApi.Models;

namespace WebApi.Tests;

[TestFixture]
public class UserRepositoryTest
{
    private static IConfiguration InitConfiguration()
    {
        var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddEnvironmentVariables()
            .Build();
        return config;
    }
    private readonly UserRepository _userRepository = new(InitConfiguration());

    [Test]
    public void GetAllUsers_Always_ReturnsUsers()
    {
        var result = _userRepository.GetAllUsers();

        var enumerable = result as User[] ?? result.ToArray();

        var user = enumerable.FirstOrDefault();



        Assert.That(result, Is.InstanceOf<IEnumerable<User>>());
    }

    [TestCase("test")]
    [TestCase("adam_nowak")]
    public void GetByLogin_ValidLogin_ReturnsUser(string login)
    {
        var result = _userRepository.GetByLogin(login);

        Assert.That(result, Is.InstanceOf<User>());
    }

    [Test]
    public void GetByLogin_InvalidLogin_ReturnsNull()
    {
        var result = _userRepository.GetByLogin("invalid_login");

        Assert.That(result, Is.Null);
    }

    [TestCase("adam.nowak@mail.pl")]
    [TestCase("pfoe.mfii@proton.me")]
    public void GetByEmail_ValidEmail_ReturnsTrue(string email)
    {
        var result = _userRepository.CheckEmail(email);

        Assert.That(result, Is.True);
    }

    [Test]
    public void GetByEmail_InvalidEmail_ReturnsFalse()
    {
        var result = _userRepository.CheckEmail("invalid_email");

        Assert.That(result, Is.False);
    }
    
    [Test]
    public void AddNewUser_ValidUser_ReturnsUser()
    {
        var newUser = new User
        {
            Login = "newuser",
            FirstName = "newuserfirstname",
            LastName = "newuserlastname",
            Password = "newuserpassword",
            Email = "newuser@example.com",
        };
        
        var result = _userRepository.AddNewUser(newUser);
        
        Assert.That(result, Is.Not.Null);
        Assert.That(newUser.Id.Equals(result.Id));
    }

}