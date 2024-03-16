using NUnit.Framework;
using WebApi.Services;
using WebApi.Models;

namespace WebApi.Tests
{
    [TestFixture]
    public class UserServiceTests
    {
        private IConfiguration _configuration;
        private UserService _userService;
        public static IConfiguration InitConfiguration()
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
            _userService = new UserService(_configuration);
        }
        

        [Test]
        public void CheckEmail_ExistingEmail_ReturnsTrue()
        {
            const string existingEmail = "existing@example.com";
            
            var result = _userService.CheckEmail(existingEmail);

            Assert.That(result, Is.True);
        }

        [Test]
        public void CheckEmail_NonExistingEmail_ReturnsFalse()
        {
            const string nonExistingEmail = "nonexisting@example.com";
            
            var result = _userService.CheckEmail(nonExistingEmail);

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
            
            var result = _userService.AddNewUser(newUser);
            
            Assert.That(result, Is.Not.Null);
            Assert.That(newUser.Id.Equals(result.Id));
        }

        [Test]
        public void AddNewUser_DuplicateLogin_ReturnsNull()
        {
            const string existingLogin = "existinglogin";
            _userService.AddNewUser(new User
            {
                Login = existingLogin,
                FirstName = "existinguserfirstname",
                LastName = "existinguserlastname",
                Password = "existinguserpassword",
                Email = "existinguser@example.com",
            });
            
            var newUser = new User
            {
                Login = existingLogin,
                FirstName = "newuserfirstname",
                LastName = "newuserlastname",
                Password = "newuserpassword",
                Email = "newuser@example.com",
            };
            
            var result = _userService.AddNewUser(newUser);
            
            Assert.That(result, Is.Null);
        }

        [Test]
        public void AddNewUser_DuplicateEmail_ReturnsNull()
        {
            const string existingEmail = "existingemail";
            _userService.AddNewUser(new User
            {
                Login = "existinguserlogin",
                FirstName = "existinguserfirstname",
                LastName = "existinguserlastname",
                Password = "existinguserpassword",
                Email = existingEmail,
            });
            
            var newUser = new User
            {
                Login = "newuserlogin",
                FirstName = "newuserfirstname",
                LastName = "newuserlastname",
                Password = "newuserpassword",
                Email = existingEmail,
            };
            
            var result = _userService.AddNewUser(newUser);
            
            Assert.That(result, Is.Null);
        }
    }
    
}