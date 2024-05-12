using System.Globalization;
using NUnit.Framework;
using WebApi.Enums;
using WebApi.Services;

namespace WebApi.Tests;

[TestFixture]
public class EmailServiceTest
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
    public void TestSending()
    {
        TextInfo textInfo = new CultureInfo("pl-PL", false).TextInfo;
        EmailService.SendEmailByType("gabap63334@bizatop.com", "Vagap", textInfo.ToTitleCase(nameof(EmailType.REGISTRATION).ToLower()).Replace("_", ""), "vagap");
        EmailService.SendEmailByType("daschel.dah@farmoaks.com",  "Daschel", textInfo.ToTitleCase(nameof(EmailType.REGISTRATION).ToLower()).Replace("_", ""), "daschel");
        EmailService.SendEmailByType("rivivot506@artgulin.com",  "Rivivot", textInfo.ToTitleCase(nameof(EmailType.REGISTRATION).ToLower()).Replace("_", ""), "rivivot");
    }
}