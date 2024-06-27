using Microsoft.AspNetCore.Mvc;
using RHP.Entities.Models.DTOs;
using RHP.Tests.Setup;
using Microsoft.Extensions.DependencyInjection;
using RHP.Entities.Models;
using RHP.Data;

namespace RHP.UnitTests
{
    public class AuthenticationControllerTests: IClassFixture<TestSetup>
    {
        private AuthenticationController controller;
        private readonly ApplicationDbContext _context;

        public AuthenticationControllerTests(TestSetup setup)
        {
            controller = setup.ServiceProvider.GetRequiredService<AuthenticationController>();
            _context = setup.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        }

        private void AddTestUserToDatabase()
        {
            // Add the User to the database
            var passwordHash = BCrypt.Net.BCrypt.HashPassword("Password");
            _context.Users.Add(new User
            {
                Email = "test@example.com",
                Password = passwordHash,
                lastLogin = DateTime.Now

            });
            _context.SaveChanges();
        }


        [Fact]
        public void Login_Should_Return_Valid_Token()
        {
            AddTestUserToDatabase();
            var dto = new UserLoginDTO 
            {
                Email = "test@example.com",
                Password = "Password"
            };

            var result = controller.Login(dto);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult.Value);
            Assert.IsType<string>(okResult.Value);
        }

        [Fact]
        public void Login_Should_Return_BadRequest_When_Invalid_Username()
        {
            AddTestUserToDatabase();

            var dto = new UserLoginDTO
            {
                Email = "invalid@example.com",
                Password = "Password"
            };


            var result = controller.Login(dto);

            var badRequestResult = result as BadRequestObjectResult;
            Assert.NotNull(badRequestResult);
            Assert.Equal(400, badRequestResult.StatusCode);
            Assert.Equal("Invalid username", badRequestResult.Value);
        }

        [Fact]
        public void Login_Should_Return_BadRequest_When_Invalid_Password()
        {
            AddTestUserToDatabase();

            var dto = new UserLoginDTO
            {
                Email = "test@example.com",
                Password = "invalidPassword"
            };

            var result = controller.Login(dto);

            var badRequestResult = result as BadRequestObjectResult;
            Assert.NotNull(badRequestResult);
            Assert.Equal(400, badRequestResult.StatusCode);
            Assert.Equal("Invalid Password", badRequestResult.Value);
        }
    }
}