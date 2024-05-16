using Microsoft.EntityFrameworkCore;
using RHP.API.Repositories;
using RHP.Data;
using RHP.Entities.Models;

namespace RHP.UnitTests
{
    public class HallRepositoryTest
    {
        private HallRepository _hallRepo;
        private ApplicationDbContext _context;
        private int FirstHallId;
        private int SecondHallId;

        public HallRepositoryTest()
        {
            var dbName = Guid.NewGuid().ToString();

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;

            _context = new ApplicationDbContext(options);
            _hallRepo = new HallRepository(_context);

            SetupTestData();
        }

        private ICollection<Player> GetPlayers()
        {
            return new Player[]
            {
        new Player { Name = "Player1", User = new User { Email = "player1@Email.com", Password = "Password", active = true } },
        new Player { Name = "Player2", User = new User { Email = "player2@Email.com", Password = "Password", active = true } }
            };

        }

        private Player GetGameMaster()
        {
            return new Player { Name = "GameMaster", User = new User { Email = "gm@Email.com", Password = "Password", active = true } };
        }

        private void SetupTestData()
        {
            var halls = new List<Hall>
        {
            new Hall { Title = "Hall1", GameMaster = GetGameMaster(), Players = GetPlayers() },
            new Hall { Title = "Hall2", GameMaster = GetGameMaster(), Players = GetPlayers() },
            new Hall { Title = "Hall3", GameMaster = GetGameMaster(), Players = GetPlayers() }
        };
            _context.Hall.AddRange(halls);
            _context.SaveChanges();

            FirstHallId = halls[0].Id;
            SecondHallId = halls[1].Id;

        }


        [Fact]
        public void Add_HallIsNotNull_AddsHallToContext()
        {
            var hall = new Hall { Title = "Hall4", GameMaster = GetGameMaster(), Players = GetPlayers() };

            _hallRepo.Add(hall);
            _context.SaveChanges();

            Assert.Contains(hall, _context.Hall);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsAllHalls()
        {
            var halls = await _hallRepo.GetAllAsync();

            Assert.Equal(3, halls.Count);
        }

        [Fact]
        public void GetById_ValidId_ReturnsHall()
        {
            var hall = _hallRepo.GetById(FirstHallId);

            Assert.NotNull(hall);
            Assert.Equal(FirstHallId, hall.Id);
        }

        [Fact]
        public void Remove_ValidId_RemovesHall()
        {
            var hall = _hallRepo.GetById(FirstHallId);
            _hallRepo.Remove(hall.Id);
            _context.SaveChanges();

            Assert.DoesNotContain(hall, _context.Hall);
        }

        [Fact]
        public void Update_HallExists_UpdatesHall()
        {
            var hall = _context.Hall.First(h => h.Id == FirstHallId);
            hall.Title = "Hall10";
            _hallRepo.Update(hall);
            _context.SaveChanges();

            Assert.Equal("Hall10", _context.Hall.First(h => h.Id == FirstHallId).Title);
        }

        [Fact]
        public void Select_ValidPredicate_ReturnsHall()
        {
            var hall = _hallRepo.Select(h => h.Id == FirstHallId);

            Assert.NotNull(hall);
            Assert.Equal(FirstHallId, hall.Id);
        }

        [Fact]
        public async Task SelectAsync_ValidPredicate_ReturnsHall()
        {
            var hall = await _hallRepo.SelectAsync(h => h.Id == FirstHallId);

            Assert.NotNull(hall);
            Assert.Equal(FirstHallId, hall.Id);
        }

        [Fact]
        public void GetByIdWithIncludes_ValidIds_ReturnsHalls()
        {
            var halls = _hallRepo.GetByIdWithIncludes(new int[] { FirstHallId, SecondHallId });

            Assert.Equal(2, halls.Length);
        }

        [Fact]
        public async Task GetByIdWithIncludesAsync_ValidIds_ReturnsHalls()
        {
            var halls = await _hallRepo.GetByIdWithIncludesAsync(new int[] { FirstHallId, SecondHallId });

            Assert.Equal(2, halls.Length);
        }
    }
}