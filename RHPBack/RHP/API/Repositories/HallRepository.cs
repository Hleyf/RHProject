using RHP.Data;
using RHP.Entities.Models;
using RHP.Entities.Models.DTOs;

namespace RHP.API.Repositories
{
    public class HallRepository : GenericRepository<Hall>
    {

        private readonly ApplicationDbContext _context;

        public HallRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<HallDTO> GetAllDto()
        {
            return _context.Hall.Select(h => new HallDTO
            {
                Id = h.Id,
                Title = h.Title,
                Description = h.Description ?? string.Empty,
                GameMasterId = h.GameMaster.Id,
                NumberOfPlayers = h.Players.Count(),
            });

        }
    }
}
