using Microsoft.EntityFrameworkCore;
using RHP.Data;
using RHP.Entities.Models;

namespace RHP.API.Repositories
{
    public class PlayerRepository : GenericRepository<Player>
    {

        private readonly ApplicationDbContext _context;

        public PlayerRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public Player? GetByName(string name)
        {
            return _context.Player
                .Include(p => p.Halls)
                .FirstOrDefault(p => p.Name == name);
        }

    }
}
