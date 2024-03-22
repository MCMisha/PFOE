using NUnit.Framework;
using WebApi.Models;
using WebApi.Repositories;

namespace WebApi.Tests;

[TestFixture]
public class EventRepositoryTest
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
        var eventRepository = new EventRepository(InitConfiguration());

        var result = eventRepository.GetAll();

        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.InstanceOf<List<Event>>());
    }

    [Test]
    public void GetById_IdExists_ReturnsEvent()
    {
        var eventRepository = new EventRepository(InitConfiguration());
        var newEvent = new Event
        {
            Id = -12,
            Name = "New Event",
            Location = "New Location",
            Category = "New Category",
            Date = new DateOnly(2022, 1, 1),
            ParticipantNumber = 10,
            Organizer = 1,
            VisitsNumber = 10,
            CreationDate = DateTime.Now
        };
        eventRepository.Add(newEvent);

        var result = eventRepository.GetById(newEvent.Id);

        Assert.That(result, Is.Not.Null);

        eventRepository.Delete(result);
    }

    [Test]
    public void GetById_IdDoesNotExist_ReturnsNull()
    {
        var eventRepository = new EventRepository(InitConfiguration());
        var result = eventRepository.GetById(-1);

        Assert.That(result, Is.Null);
    }

    [Test]
    public void Add_NewEvent_CreatesEvent()
    {
        var eventRepository = new EventRepository(InitConfiguration());
        var newEvent = new Event
        {
            Id = -13,
            Name = "New Event",
            Location = "New Location",
            Category = "New Category",
            Date = new DateOnly(2022, 1, 1),
            ParticipantNumber = 10,
            Organizer = 1,
            VisitsNumber = 10,
            CreationDate = DateTime.Now
        };

        eventRepository.Add(newEvent);
        var result = eventRepository.GetById(newEvent.Id);

        Assert.That(result, Is.Not.Null);

        eventRepository.Delete(result);
    }

    [Test]
    public void Update_ExistingEvent_UpdatesEvent()
    {
        var eventRepository = new EventRepository(InitConfiguration());
        var newEvent = new Event
        {
            Id = -14,
            Name = "New Event",
            Location = "New Location",
            Category = "New Category",
            Date = new DateOnly(2022, 1, 1),
            ParticipantNumber = 10,
            Organizer = 1,
            VisitsNumber = 10,
            CreationDate = DateTime.Now
        };
        eventRepository.Add(newEvent);
        newEvent.Name = "Updated Event";

        eventRepository.Update(newEvent);
        var result = eventRepository.GetById(newEvent.Id);

        Assert.That(result, Is.Not.Null);
        Assert.That(result.Name, Is.EqualTo("Updated Event"));

        eventRepository.Delete(result);
    }

    [Test]
    public void Delete_ExistingEvent_DeletesEvent()
    {
        var eventRepository = new EventRepository(InitConfiguration());
        var newEvent = new Event
        {
            Id = -15,
            Name = "New Event",
            Location = "New Location",
            Category = "New Category",
            Date = new DateOnly(2022, 1, 1),
            ParticipantNumber = 10,
            Organizer = 1,
            VisitsNumber = 10,
            CreationDate = DateTime.Now
        };
        eventRepository.Add(newEvent);

        eventRepository.Delete(newEvent);
        var result = eventRepository.GetById(newEvent.Id);

        Assert.That(result, Is.Null);
    }
}