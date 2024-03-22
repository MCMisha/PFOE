using NUnit.Framework;
using WebApi.Services;
using WebApi.Models;

namespace WebApi.Tests;

[TestFixture]
public class EventServiceTest
{
    private static IConfiguration InitConfiguration()
    {
        var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddEnvironmentVariables()
            .Build();
        return config;
    }

    [Test]
    public void GetAll_EventsExist_ReturnsListOfEvents()
    {
        var eventService = new EventService(InitConfiguration());

        var result = eventService.GetAll();

        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.InstanceOf<List<Event>>());
    }

    [Test]
    public void Get_IdExists_ReturnsEvent()
    {
        var eventService = new EventService(InitConfiguration());
        var newEvent = new Event
        {
            Id = -6,
            Name = "New Event",
            Location = "New Location",
            Category = "New Category",
            Date = new DateOnly(2022, 1, 1),
            ParticipantNumber = 10,
            Organizer = 1,
            VisitsNumber = 10,
            CreationDate = DateTime.Now
        };
        eventService.Add(newEvent);

        var result = eventService.Get(newEvent.Id);

        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.EqualTo(newEvent));

        eventService.Delete(newEvent.Id);
    }

    [Test]
    public void Get_IdDoesNotExist_ReturnsNull()
    {
        var eventService = new EventService(InitConfiguration());
        var result = eventService.Get(-1);

        Assert.That(result, Is.Null);
    }

    [Test]
    public void Add_Always_AddsEvent()
    {
        var eventService = new EventService(InitConfiguration());
        var newEvent = new Event
        {
            Id = -7,
            Name = "New Event",
            Location = "New Location",
            Category = "New Category",
            Date = new DateOnly(2022, 1, 1),
            ParticipantNumber = 10,
            Organizer = 1,
            VisitsNumber = 10,
            CreationDate = DateTime.Now
        };

        eventService.Add(newEvent);
        var result = eventService.Get(newEvent.Id);

        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.EqualTo(newEvent));

        eventService.Delete(newEvent.Id);
    }

    [Test]
    public void Update_EventExists_UpdatesEvent()
    {
        var eventService = new EventService(InitConfiguration());
        var newEvent = new Event
        {
            Id = -8,
            Name = "New Event",
            Location = "New Location",
            Category = "New Category",
            Date = new DateOnly(2022, 1, 1),
            ParticipantNumber = 10,
            Organizer = 1,
            VisitsNumber = 10,
            CreationDate = DateTime.Now
        };
        eventService.Add(newEvent);

        newEvent.Name = "Updated Event";
        eventService.Update(newEvent);
        var result = eventService.Get(newEvent.Id);

        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.EqualTo(newEvent));

        eventService.Delete(newEvent.Id);
    }

    [Test]
    public void Update_EventDoesNotExist_DoesNotUpdateEvent()
    {
        var eventService = new EventService(InitConfiguration());
        var newEvent = new Event
        {
            Id = -9,
            Name = "New Event",
            Location = "New Location",
            Category = "New Category",
            Date = new DateOnly(2022, 1, 1),
            ParticipantNumber = 10,
            Organizer = 1,
            VisitsNumber = 10,
            CreationDate = DateTime.Now
        };

        eventService.Update(newEvent);
        var result = eventService.Get(newEvent.Id);

        Assert.That(result, Is.Null);
    }

    [Test]
    public void Delete_ValidId_DeletesEvent()
    {
        var eventService = new EventService(InitConfiguration());
        var newEvent = new Event
        {
            Id = -10,
            Name = "New Event",
            Location = "New Location",
            Category = "New Category",
            Date = new DateOnly(2022, 1, 1),
            ParticipantNumber = 10,
            Organizer = 1,
            VisitsNumber = 10,
            CreationDate = DateTime.Now
        };
        eventService.Add(newEvent);

        eventService.Delete(newEvent.Id);
        var result = eventService.Get(newEvent.Id);

        Assert.That(result, Is.Null);
    }

    [Test]
    public void Delete_InvalidId_DoesNotDeleteEvent()
    {
        var eventService = new EventService(InitConfiguration());
        var newEvent = new Event
        {
            Id = -11,
            Name = "New Event",
            Location = "New Location",
            Category = "New Category",
            Date = new DateOnly(2022, 1, 1),
            ParticipantNumber = 10,
            Organizer = 1,
            VisitsNumber = 10,
            CreationDate = DateTime.Now
        };

        eventService.Delete(newEvent.Id);
        var result = eventService.Get(newEvent.Id);

        Assert.That(result, Is.Null);
    }

}