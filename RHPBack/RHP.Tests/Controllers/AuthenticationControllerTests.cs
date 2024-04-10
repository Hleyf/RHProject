using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using RHP.Entities.Models.DTOs;

namespace RHP.UnitTests
{
    public class AuthenticationControllerTests
    {
        private Mock<IAuthenticationService> authServiceMock;
        private AuthenticationController controller;

        public AuthenticationControllerTests()
        {
            authServiceMock = new Mock<IAuthenticationService>();
            controller = new AuthenticationController(authServiceMock.Object);
        }

        [Fact]
        public void Login_Should_Return_Valid_Token()
        {
            var dto = new UserLoginDTO
            {
                Email = "test@example.com",
                Password = "password"
            };

            var expectedToken = "validToken";

            authServiceMock.Setup(a => a.Login(dto)).Returns(expectedToken);

            var result = controller.Login(dto);

            var okResult = result as OkObjectResult;
            var value = okResult.Value as dynamic;
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
            Assert.Equal(expectedToken, value);
        }

        [Fact]
        public void Login_Should_Return_BadRequest_When_Invalid_Username()
        {
            var dto = new UserLoginDTO
            {
                Email = "invalid@example.com",
                Password = "password"
            };

            authServiceMock.Setup(a => a.Login(dto)).Throws(new Exception("Invalid username"));

            var result = controller.Login(dto);

            var badRequestResult = result as BadRequestObjectResult;
            Assert.NotNull(badRequestResult);
            Assert.Equal(400, badRequestResult.StatusCode);
            Assert.Equal("Invalid username", badRequestResult.Value);
        }

        [Fact]
        public void Login_Should_Return_BadRequest_When_Invalid_Password()
        {
            var dto = new UserLoginDTO
            {
                Email = "test@example.com",
                Password = "invalidPassword"
            };

            authServiceMock.Setup(a => a.Login(dto)).Throws(new Exception("Invalid password"));

            var result = controller.Login(dto);

            var badRequestResult = result as BadRequestObjectResult;
            Assert.NotNull(badRequestResult);
            Assert.Equal(400, badRequestResult.StatusCode);
            Assert.Equal("Invalid password", badRequestResult.Value);
        }
    }
}