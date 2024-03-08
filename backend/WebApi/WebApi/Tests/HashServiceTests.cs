using NUnit.Framework;
using WebApi.Services;

namespace WebApi.Tests;

[TestFixture]
public class HashServiceTests
{
    private HashService MakeHashService()
    {
        return new HashService();
    }

    [TestCase("password", "5e884898da28047151d0e56f8dc6292773603d0d6aabbdd62a11ef721d1542d8")]
    [TestCase("kdfsmkdsfpos", "acce4a3d1fa41fd88ef5f0340e4a77020271794ac74067ceb690f0a58e86b59e")]
    [TestCase("", "e3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855")]
    public void GetHash_WhenCalled_ReturnsHashedString(string password, string expected)
    {
        // Arrange
        var hashService = MakeHashService();

        // Act
        string result = hashService.GetHash(password);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.Not.Empty);
        Assert.That(result, Is.EqualTo(expected));
    }
}