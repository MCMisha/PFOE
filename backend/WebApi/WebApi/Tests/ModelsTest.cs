using NUnit.Framework;
using WebApi.Models;
namespace WebApi.Tests;

public enum UserProperties
{
    Id,
    Login,
    FirstName,
    LastName,
    Password,
    Email
}

public enum SettingProperties
{
    Id,
    UserId,
    Style,
    FontSize
}

public enum EventProperties
{
    Id,
    Name,
    Location,
    Category,
    Date,
    ParticipantNumber,
    Organizer,
    VisitsNumber,
    CreationDate
}

public enum ParticipantModel
{
    Id,
    UserId,
    EventId
}

public enum FailedLoginModel
{
    Id,
    FailedLoginAttempts,
    LastLoginTime,
    UserId
}

[TestFixture]
public class ModelsTest
{
    [Test]
    public void TestUserModel()
    {
        var user = new User();

        Assert.That(user, Has.Property(Enum.GetName(UserProperties.Id) ?? string.Empty));
        Assert.That(user, Has.Property(Enum.GetName(UserProperties.Login) ?? string.Empty));
        Assert.That(user, Has.Property(Enum.GetName(UserProperties.FirstName) ?? string.Empty));
        Assert.That(user, Has.Property(Enum.GetName(UserProperties.LastName) ?? string.Empty));
        Assert.That(user, Has.Property(Enum.GetName(UserProperties.Password) ?? string.Empty));
        Assert.That(user, Has.Property(Enum.GetName(UserProperties.Email) ?? string.Empty));
    }

    [Test]
    public void TestSettingModel()
    {
        var setting = new Setting();

        Assert.That(setting, Has.Property(Enum.GetName(SettingProperties.Id) ?? string.Empty));
        Assert.That(setting, Has.Property(Enum.GetName(SettingProperties.UserId) ?? string.Empty));
        Assert.That(setting, Has.Property(Enum.GetName(SettingProperties.FontSize) ?? string.Empty));
        Assert.That(setting, Has.Property(Enum.GetName(SettingProperties.Style) ?? string.Empty));
    }

    [Test]
    public void TestEventModel()
    {
        var eventModel = new Event();

        Assert.That(eventModel, Has.Property(Enum.GetName(EventProperties.Id) ?? string.Empty));
        Assert.That(eventModel, Has.Property(Enum.GetName(EventProperties.Name) ?? string.Empty));
        Assert.That(eventModel, Has.Property(Enum.GetName(EventProperties.Location) ?? string.Empty));
        Assert.That(eventModel, Has.Property(Enum.GetName(EventProperties.Category) ?? string.Empty));
        Assert.That(eventModel, Has.Property(Enum.GetName(EventProperties.Date) ?? string.Empty));
        Assert.That(eventModel, Has.Property(Enum.GetName(EventProperties.ParticipantNumber) ?? string.Empty));
        Assert.That(eventModel, Has.Property(Enum.GetName(EventProperties.Organizer) ?? string.Empty));
        Assert.That(eventModel, Has.Property(Enum.GetName(EventProperties.VisitsNumber) ?? string.Empty));
        Assert.That(eventModel, Has.Property(Enum.GetName(EventProperties.CreationDate) ?? string.Empty));
    }

    [Test]
    public void TestParticipantModel()
    {
        var participant = new Participant();
        
        Assert.That(participant, Has.Property(Enum.GetName(ParticipantModel.Id) ?? string.Empty));
        Assert.That(participant, Has.Property(Enum.GetName(ParticipantModel.EventId) ?? string.Empty));
        Assert.That(participant, Has.Property(Enum.GetName(ParticipantModel.UserId) ?? string.Empty));
    }

    [Test]
    public void TestFailedLoginModel()
    {
        var failedLogin = new FailedLogin();

        Assert.That(failedLogin, Has.Property(Enum.GetName(FailedLoginModel.Id) ?? string.Empty));
        Assert.That(failedLogin, Has.Property(Enum.GetName(FailedLoginModel.FailedLoginAttempts) ?? string.Empty));
        Assert.That(failedLogin, Has.Property(Enum.GetName(FailedLoginModel.LastLoginTime) ?? string.Empty));
        Assert.That(failedLogin, Has.Property(Enum.GetName(FailedLoginModel.UserId) ?? string.Empty));
    }

}