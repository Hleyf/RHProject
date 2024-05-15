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
        new Player { name = "Player1", user = new User { email = "player1@mail.com", password = "password", active = true } },
        new Player { name = "Player2", user = new User { email = "player2@mail.com", password = "password", active = true } }
            };

        }

        private Player GetGameMaster()
        {
            return new Player { name = "gameMaster", user = new User { email = "gm@mail.com", password = "password", active = true } };
        }

        private void SetupTestData()
        {
            var halls = new List<Hall>
        {
            new Hall { title = "Hall1", gameMaster = GetGameMaster(), players = GetPlayers() },
            new Hall { title = "Hall2", gameMaster = GetGameMaster(), players = GetPlayers() },
            new Hall { title = "Hall3", gameMaster = GetGameMaster(), players = GetPlayers() }
        };
            _context.Hall.AddRange(halls);
            _context.SaveChanges();

            FirstHallId = halls[0].id;
            SecondHallId = halls[1].id;

        }


        [Fact]
        public void Add_HallIsNotNull_AddsHallToContext()
        {
            var hall = new Hall { title = "Hall4", gameMaster = GetGameMaster(), players = GetPlayers() };

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
            Assert.Equal(FirstHallId, hall.id);
        }

        [Fact]
        public void Remove_ValidId_RemovesHall()
        {
            var hall = _hallRepo.GetById(FirstHallId);
            _hallRepo.Remove(hall.id);
            _context.SaveChanges();

            Assert.DoesNotContain(hall, _context.Hall);
        }

        [Fact]
        public void Update_HallExists_UpdatesHall()
        {
            var hall = _context.Hall.First(h => h.id == FirstHallId);
            hall.title = "Hall10";
            _hallRepo.Update(hall);
            _context.SaveChanges();

            Assert.Equal("Hall10", _context.Hall.First(h => h.id == FirstHallId).title);
        }

        [Fact]
        public void Select_ValidPredicate_ReturnsHall()
        {
            var hall = _hallRepo.Select(h => h.id == FirstHallId);

            Assert.NotNull(hall);
            Assert.Equal(FirstHallId, hall.id);
        }

        [Fact]
        public async Task SelectAsync_ValidPredicate_ReturnsHall()
        {
            var hall = await _hallRepo.SelectAsync(h => h.id == FirstHallId);

            Assert.NotNull(hall);
            Assert.Equal(FirstHallId, hall.id);
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