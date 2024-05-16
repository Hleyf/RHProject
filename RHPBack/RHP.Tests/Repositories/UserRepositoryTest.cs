using Microsoft.EntityFrameworkCore;
using RHP.API.Repositories;
using RHP.Data;
using RHP.Entities.Models;

namespace RHP.UnitTests
{
    public class UserRepositoryTest
    {
        private UserRepository _userRepo;
        private ApplicationDbContext _context;


        public UserRepositoryTest()
        {

            var dbName = Guid.NewGuid().ToString();

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;

            _context = new ApplicationDbContext(options);
            _userRepo = new UserRepository(_context);

            SetupTestData();

        }

        private void SetupTestData()
        {
            _context.User.AddRange(
                new User { Email = "test1@Email.com", Password = "Password", active = true },
                new User { Email = "test2@Email.com", Password = "Password", active = true },
                new User { Email = "test3@Email.com", Password = "Password", active = true }
            );
            _context.SaveChanges();
        }

        [Fact]
        public void Add_UserIsNotNull_AddsUserToContext()
        {
            var user = new User { Email = "test4@Email.com", Password = "Password", active = true };

            _userRepo.Add(user);
            _context.SaveChanges();

            Assert.Contains(user, _context.User);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsAllUsers()
        {
            var users = await _userRepo.GetAllAsync();

            Assert.Equal(3, users.Count);
        }

        [Fact]
        public void GetById_ValidId_ReturnsUser()
        {
            var user = _userRepo.GetById(1);

            Assert.NotNull(user);
            Assert.Equal(1, user.Id);
        }

        [Fact]
        public void Remove_ValidId_RemovesUser()
        {
            var user = _userRepo.GetById(1);
            _userRepo.Remove(user.Id);
            _context.SaveChanges();

            Assert.DoesNotContain(user, _context.User);
        }

        [Fact]
        public void Update_UserExists_UpdatesUser()
        {
            var user = _context.User.First(u => u.Id == 1);
            user.Email = "test10@Email.com";
            _userRepo.Update(user);
            _context.SaveChanges();

            Assert.Equal("test10@Email.com", _context.User.First(u => u.Id == 1).Email);
        }

        [Fact]
        public void Select_ValidPredicate_ReturnsUser()
        {
            var user = _userRepo.Select(u => u.Id == 1);

            Assert.NotNull(user);
            Assert.Equal(1, user.Id);
        }

        [Fact]
        public async Task SelectAsync_ValidPredicate_ReturnsUser()
        {
            var user = await _userRepo.SelectAsync(u => u.Id == 1);

            Assert.NotNull(user);
            Assert.Equal(1, user.Id);
        }

        [Fact]
        public void GetByIdWithIncludes_ValidIds_ReturnsUsers()
        {
            var users = _userRepo.GetByIdWithIncludes(new int[] { 1, 2 });

            Assert.Equal(2, users.Length);
        }

        [Fact]
        public async Task GetByIdWithIncludesAsync_ValidIds_ReturnsUsers()
        {
            var users = await _userRepo.GetByIdWithIncludesAsync(new int[] { 1, 2 });

            Assert.Equal(2, users.Length);
        }
    }
}