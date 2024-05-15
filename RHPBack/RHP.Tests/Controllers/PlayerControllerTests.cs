using RHP.API.Controllers;
using Microsoft.AspNetCore.Mvc;
using RHP.Tests.Setup;
using Microsoft.Extensions.DependencyInjection;
using RHP.Entities.Models;

namespace RHP.UnitTests
{
    public class PlayerControllerTests : IClassFixture<TestSetup>
    {
        private readonly PlayerController _controller;
        private readonly Data.ApplicationDbContext _context;

        public PlayerControllerTests(TestSetup setup)
        {
            _controller = setup.ServiceProvider.GetRequiredService<PlayerController>();
            _context = setup.ServiceProvider.GetRequiredService<Data.ApplicationDbContext>();
        }

        private void AddTestPlayerToDatabase()
        {
            var passwordHash = BCrypt.Net.BCrypt.HashPassword("password");
            User user = new User
            {
                email = "test@email.com",
                password = passwordHash
            };
            _context.Player.Add(new Player
            {
                name = "Test player",
                user = user
            });
        }

        [Fact]
        public void CreatePlayerUser_ReturnsBadRequest_WhenDtoIsNull()
        {
            AddTestPlayerToDatabase();

            var result = _controller.CreatePlayerUser(null);

            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public void CreatePlayerUser_ReturnsOk_WhenDtoIsNotNull()
        {
            var dto = new UserPlayerDTO
            {
                name = "Test player",
                email = "testplayer@example.com",
                password = "TestPassword123"
            };

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
                name = "",
                email = "",
                password = ""
            };

            var result = _controller.CreatePlayerUser(dto);

            Assert.IsType<BadRequestResult>(result);

        }
    }
}
