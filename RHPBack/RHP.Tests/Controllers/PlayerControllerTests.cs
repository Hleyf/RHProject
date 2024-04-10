using Xunit;
using Moq;
using RHP.API.Controllers;
using RHP.API.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using RHP.Entities.Models;

namespace RHP.UnitTests
{
    public class PlayerControllerTests
    {
        private readonly Mock<IPlayerService> _mockPlayerService;
        private readonly PlayerController _controller;

        public PlayerControllerTests()
        {
            _mockPlayerService = new Mock<IPlayerService>();
            _controller = new PlayerController(_mockPlayerService.Object);
        }

        [Fact]
        public void CreatePlayerUser_ReturnsBadRequest_WhenDtoIsNull()
        {
            var result = _controller.CreatePlayerUser(null);

            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public void CreatePlayerUser_ReturnsOk_WhenDtoIsNotNull()
        {
            var dto = new UserPlayerDTO
            {
                PlayerName = "Test Player",
                Email = "testplayer@example.com",
                Password = "TestPassword123"
            };

            Player player = null;

            _mockPlayerService.Setup(service => service.CreatePlayer(dto))
                .Callback<UserPlayerDTO>(inputDto => player = new Player
                {
                    Id = 1,
                    Name = inputDto.PlayerName,
                    User = new User
                    {
                        Email = inputDto.Email,
                        Password = inputDto.Password
                    }
                });

            var result = _controller.CreatePlayerUser(dto);

            var okResult = Assert.IsType<OkResult>(result);

            Assert.NotNull(result);
            Assert.IsType<OkResult>(result);
        }


        [Fact]
        public void CreatePlayerUser_ReturnsBadRequest_WhenServiceThrowsException()
        {
            var dto = new UserPlayerDTO
            {
                PlayerName = "Test Player",
                Email = "testplayer@example.com",
                Password = "TestPassword123"
            };

            _mockPlayerService.Setup(service => service.CreatePlayer(dto))
                .Throws(new InvalidOperationException("Test exception"));

            var result = _controller.CreatePlayerUser(dto);

            Assert.IsType<BadRequestResult>(result);

        }
    }
}
