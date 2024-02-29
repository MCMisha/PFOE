using NUnit.Framework;
using WebApi.Contexts;
using Assert = NUnit.Framework.Assert;

namespace WebApi.Tests;

[TestFixture]
public class AppDbContextTest
{
    private IConfiguration _configuration;
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
    }

    [Test]
    public void TestConnection()
    {
        AppDbContext appDbContext = new AppDbContext(_configuration);
        Assert.That(appDbContext.Database.CanConnect, Is.EqualTo(true));
    }
    
    [Test]
    public void CheckIsDbSetUsersExists()
    {
        AppDbContext appDbContext = new AppDbContext(_configuration);
        Assert.That(appDbContext.Users, Is.Not.EqualTo(null));
    }

    [Test]
    public void CheckIsDbSetEventsExists()
    {
        var appDbContext = new AppDbContext(_configuration);
        Assert.That(appDbContext.Events, Is.Not.EqualTo(null));
    }
    
    [Test]
    public void CheckIsDbSetSettingsExists()
    {
        var appDbContext = new AppDbContext(_configuration);
        Assert.That(appDbContext.Settings, Is.Not.EqualTo(null));
    }
    
    [Test]
    public void CheckIsDbSetParticipantsExists()
    {
        var appDbContext = new AppDbContext(_configuration);
        Assert.That(appDbContext.Participants, Is.Not.EqualTo(null));
    }
    
    [Test]
    public void CheckIsDbSetFailedLoginsExists()
    {
        var appDbContext = new AppDbContext(_configuration);
        Assert.That(appDbContext.FailedLogins, Is.Not.EqualTo(null));
    }
}