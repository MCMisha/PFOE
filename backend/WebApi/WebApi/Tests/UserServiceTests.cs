using WebApi.Services;
using WebApi.Models;
using NUnit.Framework;

namespace WebApi.Tests;

public class UserServiceTests
{
    private class MockRepository : IUserRepository
    {
        private List<User> _users;

        public MockUserService()
        {
            _users = new List<User>
            {
                new User
                {
                    Id = 1,
                    Login = "good",
                    Password = "good"
                }
            };
        }

        public User GetUserByUserName(string userName)
        {
            return _users.FirstOrDefault(u => u.Login == userName);
        }
    }

    private UserService MakeService()
    {
        var mockRepository = new MockRepository();
        return new UserService(mockRepository);
    }

    [Test]
    public void Login_ValidCredentials_ReturnsTrue()
    {
        var service = MakeService();

        var result = service.Login("good", "good");

        Assert.IsTrue(result);
    }

    [TestCase("bad", "bad")]
    [TestCase("good", "bad")]
    [TestCase("bad", "good")]
    public void Login_InvalidCredentials_ReturnsFalse(string username, string password)
    {
        var service = MakeService();

        var result = service.Login(username, password);

        Assert.IsFalse(result);
    }
}