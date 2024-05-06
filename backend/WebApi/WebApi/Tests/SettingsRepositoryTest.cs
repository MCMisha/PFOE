using NUnit.Framework;
using WebApi.Models;
using WebApi.Repositories;

namespace WebApi.Tests;

[TestFixture]
public class SettingsRepositoryTest
{
    private static IConfiguration InitConfiguration()
    {
        var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddEnvironmentVariables()
            .Build();
        return config;
    }

    [TestCase(1)]
    [TestCase(2)]
    [TestCase(12)]
    public void GetByUserId_ValidUserId_ReturnsSettings(int id)
    {
        var settingsRepository = new SettingsRepository(InitConfiguration());

        var result = settingsRepository.GetByUserId(id);

        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.InstanceOf<Setting>());
    }

    [TestCase(-1)]
    [TestCase(3)]
    [TestCase(100)]
    public void GetByUserId_InvalidUserId_ReturnsSettings(int id)
    {
        var settingsRepository = new SettingsRepository(InitConfiguration());

        var result = settingsRepository.GetByUserId(id);

        Assert.That(result, Is.Null);
    }

    [Test]
    public void Update_ValidSettings_UpdatesSettings()
    {
        const int userId = 1;

        var expected = new Setting
        {
            Id = 1,
            FontSize = 999,
            Style = "test",
            UserId = userId
        };

        var settingsRepository = new SettingsRepository(InitConfiguration());

        settingsRepository.Update(expected);

        var result = settingsRepository.GetByUserId(userId);

        Assert.That(result, Is.Not.Null);

        Assert.That(result!.Id, Is.EqualTo(expected.Id));
        Assert.That(result.FontSize, Is.EqualTo(expected.FontSize));
        Assert.That(result.Style, Is.EqualTo(expected.Style));
        Assert.That(result.UserId, Is.EqualTo(expected.UserId));
    }

}