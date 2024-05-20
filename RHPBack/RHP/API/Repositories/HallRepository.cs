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


        public async Task<IEnumerable<Hall>> GetAllWithRelationships()
        {
            return await _context.Set<Hall>()
                .Include(h => h.Players)
                .ThenInclude(p => p.User).ToListAsync();
        }

        public async Task<Hall> GetWithRelationships(int id)
        {
            var hall = await _context.Set<Hall>()
                .Include(h => h.Players)
                .ThenInclude(p => p.User)
                .FirstOrDefaultAsync(h => h.Id == id);

            if (hall is null)
            {
                throw new Exception($"No Hall found with ID {id}");
            }

            return hall;
        }
    }
}
