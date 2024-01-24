using RHP.Data;
using RHP.Models;

namespace RHP.Repositories
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
