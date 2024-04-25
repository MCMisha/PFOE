using System.Diagnostics;
using NUnit.Framework;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Tests;

[TestFixture]
public class SettingServiceTest
{
    private static IConfiguration InitConfiguration()
    {
        var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddEnvironmentVariables()
            .Build();
        return config;
    }

    private SettingsService _settingsService;

    [SetUp]
    public void Setup()
    {
        _settingsService = new SettingsService(InitConfiguration());
    }

    [TestCase(1)]
    [TestCase(2)]
    [TestCase(12)]
    public void Get_ValidUserId_ReturnsSetting(int id)
    {
        var result = _settingsService.Get(id);

        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.InstanceOf<Setting>());
    }

    [TestCase(-1)]
    [TestCase(3)]
    [TestCase(100)]
    public void Get_InvalidUserId_ReturnsNull(int id)
    {
        var result = _settingsService.Get(id);

        Assert.That(result, Is.Null);
    }

    [Test]
    public void Update_ValidSettings_UpdatesSettings()
    {
        const int testedUserId = 1;

        var oldSettings = _settingsService.Get(testedUserId);

        Debug.Assert(oldSettings != null, nameof(oldSettings) + " != null");

        var testSettings = new Setting
            { Id = oldSettings.Id, FontSize = 17, Style = "dark", UserId = oldSettings.UserId };

        _settingsService.Update(testSettings);

        var result = _settingsService.Get(testedUserId);

        Assert.That(result, Is.Not.Null);
        Assert.That(result!.Id, Is.EqualTo(testSettings.Id));
        Assert.That(result.FontSize, Is.EqualTo(testSettings.FontSize));
        Assert.That(result.Style, Is.EqualTo(testSettings.Style));
        Assert.That(result.UserId, Is.EqualTo(testSettings.UserId));
    }

    [Test]
    public void Update_InvalidSettings_DoesNothing()
    {
        const int userId = 1;

        var actual = _settingsService.Get(userId);

        var settings = new Setting
        {
            Id = 1,
            FontSize = -20,
            Style = "test",
            UserId = -userId
        };

        _settingsService.Update(settings);

        Debug.Assert(actual != null, nameof(actual) + " != null");
        Assert.That(actual.FontSize, Is.Not.EqualTo(-20));
        Assert.That(actual.Style, Is.Not.EqualTo("test"));
        Assert.That(actual.UserId, Is.Not.EqualTo(-1));
    }
}