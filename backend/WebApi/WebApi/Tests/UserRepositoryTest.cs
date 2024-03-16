using NUnit.Framework;
using WebApi.Repositories;
using WebApi.Models;

namespace WebApi.Tests;

[TestFixture]
public class UserRepositoryTest
{
    
    private IConfiguration _configuration;
    private UserRepository _userRepository;
    private static IConfiguration InitConfiguration()
    {
        var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddEnvironmentVariables()
            .Build();
        return config;
    }
    
    [SetUp]
    public void Setup()
    {
        _configuration = InitConfiguration();
        _userRepository = new UserRepository(_configuration);
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