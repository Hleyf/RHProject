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
                .FirstOrDefault(p => p.Name.Equals(name));
        }

        public IEnumerable<Player> GetAllActive()
        {
            return _context.Player
               .Include(p => p.Halls)
               .Include(p => p.User)
               .Where(p => p.User.active.Equals(true));
        }

        internal Player? GetPlayerByName(string name)
        {
            return _context.Player
                .Include(p => p.Halls)
                .FirstOrDefault(p => p.Name.Equals(name));
        }

        internal Player GetPlayerByUserId(int userId)
        {
            Player? player = _context.Player
                .Include(u => u.User)
                .FirstOrDefault(u => u.User.Id.Equals(userId));

            if (player == null)
            {
                throw new Exception("Player not found");
            }
            return player;
        }
    }
}
