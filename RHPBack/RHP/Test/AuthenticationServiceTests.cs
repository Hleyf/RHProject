using NUnit.Framework;
using Moq;
using Microsoft.AspNetCore.Mvc;
using RHP.Entities.Models.DTOs;
using NUnit.Framework.Legacy;

[TestFixture]
public class AuthenticationControllerTests
{
    private Mock<IAuthenticationService> authServiceMock;
    private AuthenticationController controller;

    [SetUp]
    public void Setup()
    {
        authServiceMock = new Mock<IAuthenticationService>();
        controller = new AuthenticationController(authServiceMock.Object);
    }

    [Test]
    public void Login_Should_Return_Valid_Token()
    {
        // Arrange
        var dto = new UserLoginDTO
        {
            Email = "test@example.com",
            Password = "password"
        };

        var expectedToken = "validToken";

        authServiceMock.Setup(a => a.Login(dto)).Returns(expectedToken);

        // Act
        var result = controller.Login(dto);

        // Assert
        var okResult = result as OkObjectResult;
        ClassicAssert.NotNull(okResult);
        Assert.That(okResult.StatusCode, Is.EqualTo(200));
        Assert.That(okResult.Value, Is.EqualTo(expectedToken));
    }

    [Test]
    public void Login_Should_Return_BadRequest_When_Invalid_Username()
    {
        // Arrange
        var dto = new UserLoginDTO
        {
            Email = "invalid@example.com",
            Password = "password"
        };

        authServiceMock.Setup(a => a.Login(dto)).Throws(new Exception("Invalid username"));

        // Act
        var result = controller.Login(dto);

        // Assert
        var badRequestResult = result as BadRequestObjectResult;
        ClassicAssert.NotNull(badRequestResult);
        Assert.That(badRequestResult.StatusCode, Is.EqualTo(400));
        Assert.That(badRequestResult.Value, Is.EqualTo("Invalid username"));
    }

    [Test]
    public void Login_Should_Return_BadRequest_When_Invalid_Password()
    {
        // Arrange
        var dto = new UserLoginDTO
        {
            Email = "test@example.com",
            Password = "invalidPassword"
        };

        authServiceMock.Setup(a => a.Login(dto)).Throws(new Exception("Invalid password"));

        // Act
        var result = controller.Login(dto);

        // Assert
        var badRequestResult = result as BadRequestObjectResult;
        ClassicAssert.NotNull(badRequestResult);
        Assert.That(badRequestResult.StatusCode, Is.EqualTo(400));
        Assert.That(badRequestResult.Value, Is.EqualTo("Invalid password"));
    }
}
