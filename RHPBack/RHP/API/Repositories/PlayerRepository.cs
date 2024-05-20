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

        public async Task<IEnumerable<Player>> GetAllActive()
        {
            return await _context.Player
               .Include(p => p.Halls)
               .Include(p => p.User)
               .Where(p => p.User.active.Equals(true)).ToArrayAsync();
        }

        internal async Task<Player> GetPlayerByName(string name)
        {
            Player? player =  await _context.Player
                .Include(p => p.Halls)
                .FirstOrDefaultAsync(p => p.Name.Equals(name));

            if (player is null)
            {
                throw new Exception("Player not found");
            }

            return player;
        }

        internal async Task<Player> GetPlayerByUserId(string userId)
        {
            Player? player = await _context.Player
                .Include(u => u.User)
                .FirstOrDefaultAsync(u => u.User.Id.Equals(userId));

            if (player is null)
            {
                throw new Exception("Player not found");
            }
            return player;
        }
    }
}
