using Nunit.Framework;
using WebApi.Models;
using WebApi.Controllers;

namespace WebApi.Tests;

[TestFixture]
public class UserControllerTests
{
    private class MockUserService : IUserService
    {
        private List<User> _users;

        public MockUserService()
        {
            _users = new List<User>
            {
                new User
                {
                    Id = 1,
                    Login = "user1",
                    Password = "password1"
                },
                new User
                {
                    Id = 2,
                    Login = "user2",
                    Password = "password2"
                }
            };
        }

        public bool Login(string username, string password)
        {
            var user = _users.FirstOrDefault(u => u.Login == username);
            return user != null && user.Password == password;
        }
    }

    private UserController MakeController()
    {
        var mockUserService = new MockUserService();
        return new UserController(mockUserService);
    }

    [Test]
    public void Login_ValidCredentials_ReturnsOk()
    {
        var controller = MakeController();

        var result = controller.Login("user1", "password1");
        var okResult = result as OkObjectResult;

        Assert.IsNotNull(okResult);
        Assert.AreEqual(200, okResult.StatusCode);
    }

    [Test]
    public void Login_InvalidCredentials_ReturnsUnauthorized()
    {
        var controller = MakeController();

        var result = controller.Login("user1", "password2");
        var unauthorizedResult = result as UnauthorizedObjectResult;

        Assert.IsNotNull(unauthorizedResult);
        Assert.AreEqual(401, unauthorizedResult.StatusCode);
    }
}