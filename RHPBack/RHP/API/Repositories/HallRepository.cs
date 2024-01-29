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
    }
}
