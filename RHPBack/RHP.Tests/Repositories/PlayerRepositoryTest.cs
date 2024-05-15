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
            _context.Player.AddRange(
                new Player { name = "Player1", user = new User { email = "test1@mail.com", password = "password", active = true } },
                new Player { name = "Player2", user = new User { email = "test2@mail.com", password = "password", active = true } },
                new Player { name = "Player3", user = new User { email = "test3@mail.com", password = "password", active = true } }
            );
            _context.SaveChanges();
        }

        [Fact]
        public void Add_PlayerIsNotNull_AddsPlayerToContext()
        {
            var player = new Player { name = "Player4", user = new User { email = "test4@mail.com", password = "password", active = true } };

            _playerRepo.Add(player);
            _context.SaveChanges();

            Assert.Contains(player, _context.Player);
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
            Assert.Equal(1, player.id);
        }

        [Fact]
        public void Remove_ValidId_RemovesPlayer()
        {
            var player = _playerRepo.GetById(1);
            _playerRepo.Remove(player.id);
            _context.SaveChanges();

            Assert.DoesNotContain(player, _context.Player);
        }

        [Fact]
        public void Update_PlayerExists_UpdatesPlayer()
        {
            var player = _context.Player.First(p => p.id == 1);
            player.name = "Player10";
            _playerRepo.Update(player);
            _context.SaveChanges();

            Assert.Equal("Player10", _context.Player.First(p => p.id == 1).name);
        }

        [Fact]
        public void Select_ValidPredicate_ReturnsPlayer()
        {
            var player = _playerRepo.Select(p => p.id == 1);

            Assert.NotNull(player);
            Assert.Equal(1, player.id);
        }

        [Fact]
        public async Task SelectAsync_ValidPredicate_ReturnsPlayer()
        {
            var player = await _playerRepo.SelectAsync(p => p.id == 1);

            Assert.NotNull(player);
            Assert.Equal(1, player.id);
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
            Assert.Equal("Player1", player.name);
        }

        [Fact]
        public void GetAllActive_ReturnsActivePlayers()
        {
            var players = _playerRepo.GetAllActive();

            Assert.Equal(3, players.Count());
        }
    }
}