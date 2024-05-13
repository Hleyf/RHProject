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
                .Include(h => h.Players)
                .ThenInclude(p => p.User);
        }

        public Hall GetWithRelationships(int id)
        {
            var hall = _context.Set<Hall>()
                .Include(h => h.Players)
                .ThenInclude(p => p.User)
                .FirstOrDefault(h => h.Id == id);

            if (hall == null)
            {
                throw new Exception($"No Hall found with ID {id}");
            }

            return hall;
        }
    }
}
