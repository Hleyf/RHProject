using Microsoft.EntityFrameworkCore;
using RHP.API.Repositories;
using RHP.Data;
using RHP.Entities.Models;

namespace RHP.UnitTests
{
    public class PlayerRepositoryTest
    {
        private PlayerRepository _playerRepo;
        private ApplicationDbContext _context;

        public PlayerRepositoryTest()
        {
            var dbName = Guid.NewGuid().ToString();

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;

            _context = new ApplicationDbContext(options);
            _playerRepo = new PlayerRepository(_context);

            SetupTestData();
        }

        private void SetupTestData()
        {
            _context.Players.AddRange(
                new Player { Name = "Player1", User = new User { Email = "test1@Email.com", Password = "Password", active = true, lastLogin = DateTime.Now } },
                new Player { Name = "Player2", User = new User { Email = "test2@Email.com", Password = "Password", active = true, lastLogin = DateTime.Now } },
                new Player { Name = "Player3", User = new User { Email = "test3@Email.com", Password = "Password", active = true, lastLogin = DateTime.Now } }
            );
            _context.SaveChanges();
        }

        [Fact]
        public void Add_PlayerIsNotNull_AddsPlayerToContext()
        {
            var player = new Player { Name = "Player4", User = new User { Email = "test4@Email.com", Password = "Password", active = true, lastLogin = DateTime.Now } };

            _playerRepo.Add(player);
            _context.SaveChanges();

            Assert.Contains(player, _context.Players);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsAllPlayers()
        {
            var players = await _playerRepo.GetAllAsync();

            Assert.Equal(3, players.Count);
        }

        [Fact]
        public void GetById_ValidId_ReturnsPlayer()
        {
            var player = _playerRepo.GetById(1);

            Assert.NotNull(player);
            Assert.Equal(1, player.Id);
        }

        [Fact]
        public void Remove_ValidId_RemovesPlayer()
        {
            var player = _playerRepo.GetById(1);
            _playerRepo.Remove(player.Id);
            _context.SaveChanges();

            Assert.DoesNotContain(player, _context.Players);
        }

        [Fact]
        public void Update_PlayerExists_UpdatesPlayer()
        {
            var player = _context.Players.First(p => p.Id == 1);
            player.Name = "Player10";
            _playerRepo.Update(player);
            _context.SaveChanges();

            Assert.Equal("Player10", _context.Players.First(p => p.Id == 1).Name);
        }

        [Fact]
        public void Select_ValidPredicate_ReturnsPlayer()
        {
            var player = _playerRepo.Select(p => p.Id == 1);

            Assert.NotNull(player);
            Assert.Equal(1, player.Id);
        }

        [Fact]
        public async Task SelectAsync_ValidPredicate_ReturnsPlayer()
        {
            var player = await _playerRepo.SelectAsync(p => p.Id == 1);

            Assert.NotNull(player);
            Assert.Equal(1, player.Id);
        }

        [Fact]
        public void GetByIdWithIncludes_ValidIds_ReturnsPlayers()
        {
            var players = _playerRepo.GetByIdWithIncludes(new int[] { 1, 2 });

            Assert.Equal(2, players.Length);
        }

        [Fact]
        public async Task GetByIdWithIncludesAsync_ValidIds_ReturnsPlayers()
        {
            var players = await _playerRepo.GetByIdWithIncludesAsync(new int[] { 1, 2 });

            Assert.Equal(2, players.Length);
        }

        [Fact]
        public void GetByName_ValidName_ReturnsPlayer()
        {
            var player = _playerRepo.GetByName("Player1");

            Assert.NotNull(player);
            Assert.Equal("Player1", player.Name);
        }

        [Fact]
        public async void GetAllActive_ReturnsActivePlayers()
        {
            var players = await _playerRepo.GetAllActive();
            Assert.Equal(3, players.Count());
        }
    }
}