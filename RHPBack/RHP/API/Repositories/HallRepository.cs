using Microsoft.EntityFrameworkCore;
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


        public IEnumerable<Hall> GetAllWithRelationships()
        {
            return _context.Set<Hall>()
                .Include(h => h.Players)
                .ThenInclude(p => p.User);
        }
    }
}
