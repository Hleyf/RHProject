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
                .Include(p => p.halls)
                .FirstOrDefault(p => p.name.Equals(name));
        }

        public IEnumerable<Player> GetAllActive()
        {
            return _context.Player
               .Include(p => p.halls)
               .Include(p => p.user)
               .Where(p => p.user.active.Equals(true));
        }

        internal Player? GetPlayerByName(string name)
        {
            return _context.Player
                .Include(p => p.halls)
                .FirstOrDefault(p => p.name.Equals(name));
        }

        internal Player GetPlayerByUserId(int userId)
        {
            Player? player = _context.Player
                .Include(u => u.user)
                .FirstOrDefault(u => u.user.id.Equals(userId));

            if (player == null)
            {
                throw new Exception("player not found");
            }
            return player;
        }
    }
}
