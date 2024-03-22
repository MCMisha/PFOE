using NUnit.Framework;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebApi.Controllers;
using WebApi.Models;

namespace WebApi.Tests;

[TestFixture]
public class EventControllerTest
{
    private static IConfiguration InitConfiguration()
    {
        var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddEnvironmentVariables()
            .Build();
        return config;
    }

    private readonly ILogger<EventController> _logger = new Mock<ILogger<EventController>>().Object;


    [Test]
    public void GetAll_EventsExist_ReturnsOk()
    {
        var controller = new EventController(_logger, InitConfiguration());

        var result = controller.GetAll();

        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.InstanceOf<ActionResult<List<Event>>>());
    }

    [Test]
    public void Get_IdExists_ReturnsOk()
    {
        var controller = new EventController(_logger, InitConfiguration());
        var newEvent = new Event
        {
            Id = -1,
            Name = "New Event",
            Location = "New Location",
            Category = "New Category",
            Date = new DateOnly(2022, 1, 1),
            ParticipantNumber = 10,
            Organizer = 1,
            VisitsNumber = 10,
            CreationDate = DateTime.Now
        };
        controller.Create(newEvent);

        var result = controller.Get(newEvent.Id);

        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.InstanceOf<OkObjectResult>());

        controller.Delete(controller.GetAll().Value.Last().Id);
    }

    [Test]
    public void Get_IdDoesNotExist_ReturnsNotFound()
    {
        var controller = new EventController(_logger, InitConfiguration());
        const int id = -1;

        var result = controller.Get(id);

        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.InstanceOf<NotFoundResult>());
    }

    [Test]
    public void Create_ValidEvent_ReturnsCreatedAtAction()
    {
        var controller = new EventController(_logger, InitConfiguration());
        var newEvent = new Event
        {
            Id = -2,
            Name = "New Event",
            Location = "New Location",
            Category = "New Category",
            Date = new DateOnly(2022, 1, 1),
            ParticipantNumber = 10,
            Organizer = 1,
            VisitsNumber = 10,
            CreationDate = DateTime.Now
        };

        var result = controller.Create(newEvent);


        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.InstanceOf<CreatedAtActionResult>());

        controller.Delete(controller.GetAll().Value.Last().Id);
    }

    [Test]
    public void Update_ValidEvent_ReturnsNoContent()
    {
        var controller = new EventController(_logger, InitConfiguration());
        var newEvent = new Event
        {
            Id = -3,
            Name = "New Event2",
            Location = "New Location",
            Category = "New Category",
            Date = new DateOnly(2022, 1, 1),
            ParticipantNumber = 10,
            Organizer = 1,
            VisitsNumber = 10,
            CreationDate = DateTime.Now
        };
        controller.Create(newEvent);

        newEvent.Name = "Updated Event";
        var result = controller.Update(newEvent);

        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.InstanceOf<NoContentResult>());
        Assert.That(newEvent.Name, Is.EqualTo("Updated Event"));

        controller.Delete(controller.GetAll().Value.Last().Id);
    }

    [Test]
    public void Update_InvalidEvent_ReturnsNotFound()
    {
        var controller = new EventController(_logger, InitConfiguration());
        var newEvent = new Event
        {
            Id = -4,
            Name = "New Event2",
            Location = "New Location",
            Category = "New Category",
            Date = new DateOnly(2022, 1, 1),
            ParticipantNumber = 10,
            Organizer = 1,
            VisitsNumber = 10,
            CreationDate = DateTime.Now
        };

        var result = controller.Update(newEvent);

        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.InstanceOf<NotFoundResult>());
    }

    [Test]
    public void Delete_ValidId_ReturnsNoContent()
    {
        var controller = new EventController(_logger, InitConfiguration());
        var newEvent = new Event
        {
            Id = -5,
            Name = "New Event",
            Location = "New Location",
            Category = "New Category",
            Date = new DateOnly(2022, 1, 1),
            ParticipantNumber = 10,
            Organizer = 1,
            VisitsNumber = 10,
            CreationDate = DateTime.Now
        };
        controller.Create(newEvent);

        var result = controller.Delete(newEvent.Id);

        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.InstanceOf<NoContentResult>());
        
    }

    [Test]
    public void Delete_InvalidId_ReturnsNotFound()
    {
        var controller = new EventController(_logger, InitConfiguration());
        const int id = -1;

        var result = controller.Delete(id);

        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.InstanceOf<NotFoundResult>());
    }


}