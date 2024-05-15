using Microsoft.EntityFrameworkCore;
using RHP.Data;
using RHP.Entities.Models;

namespace RHP.API.Repositories
{
    public class HallRepository : GenericRepository<Hall>
    {

        private readonly ApplicationDbContext _context;

        public HallRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }


        public IEnumerable<Hall> GetAllWithRelationships()
        {
            return _context.Set<Hall>()
                .Include(h => h.players)
                .ThenInclude(p => p.user);
        }

        public Hall GetWithRelationships(int id)
        {
            var hall = _context.Set<Hall>()
                .Include(h => h.players)
                .ThenInclude(p => p.user)
                .FirstOrDefault(h => h.id == id);

            if (hall == null)
            {
                throw new Exception($"No hall found with ID {id}");
            }

            return hall;
        }
    }
}
