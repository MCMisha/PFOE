using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using WebApi.Controllers;
using Moq;
using WebApi.Models;

namespace WebApi.Tests;

[TestFixture]
public class SettingsControllerTest
{
    private readonly ILogger<SettingsController> _logger = new Mock<ILogger<SettingsController>>().Object;

    private static IConfiguration InitConfiguration()
    {
        var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddEnvironmentVariables()
            .Build();
        return config;
    }

    private SettingsController _settingsController;

    [SetUp]
    public void Setup()
    {
        _settingsController = new SettingsController(_logger, InitConfiguration());
    }

    [TestCase(1)]
    [TestCase(2)]
    [TestCase(12)]
    public void Get_ValidUserId_ReturnsOk(int id)
    {
        var result = _settingsController.Get(id);

        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.InstanceOf<OkObjectResult>());
    }

    [TestCase(-1)]
    [TestCase(3)]
    [TestCase(100)]
    public void Get_InvalidUserId_ReturnsOk(int id)
    {
        var result = _settingsController.Get(id);

        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.InstanceOf<NotFoundResult>());
    }

    [Test]
    public void Update_ValidSettings_ReturnsOk()
    {
        const int testedUserId = 1;

        var oldSettings = (_settingsController.Get(testedUserId) as OkObjectResult)?.Value as Setting;

        if (oldSettings == null)
        {
            return;
        }

        var testSettings = new Setting
            { Id = oldSettings.Id, FontSize = 17, Style = "dark", UserId = oldSettings.UserId };
        var result = _settingsController.Update(testSettings);

        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.InstanceOf<OkResult>());

        var actual = (_settingsController.Get(testedUserId) as OkObjectResult)?.Value as Setting;

        Assert.That(actual, Is.Not.Null);
        Assert.That(actual!.Id, Is.EqualTo(testSettings.Id));
        Assert.That(actual.FontSize, Is.EqualTo(testSettings.FontSize));
        Assert.That(actual.Style, Is.EqualTo(testSettings.Style));
        Assert.That(actual.UserId, Is.EqualTo(testSettings.UserId));
    }

    [Test]
    public void Update_InvalidSettings_ReturnsNotFound()
    {
        Setting settings = new Setting
        {
            Id = -1,
            FontSize = 17,
            Style = "dark",
            UserId = -1
        };

        var result = _settingsController.Update(settings);

        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.InstanceOf<NotFoundResult>());
    }
}