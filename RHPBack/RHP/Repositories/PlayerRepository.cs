using Microsoft.EntityFrameworkCore;
using RHP.Data;
using RHP.Models;

namespace RHP.Repositories
{
    public class PlayerRepository : GenericRepository<Player>
    {

        private readonly ApplicationDbContext _context;

        public PlayerRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

    }
}
