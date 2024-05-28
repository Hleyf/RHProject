using Microsoft.EntityFrameworkCore;
using RHP.Data;
using RHP.Entities.Models;

namespace RHP.API.Repositories
{
    public class ContactRequestRepository : GenericRepository<ContactRequest>
    {
        private readonly ApplicationDbContext _context;

        public ContactRequestRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        internal async Task<List<ContactRequest>> GetAllAsyncByFromUserId(string userId)
        {
            return await _context.ContactRequest.Where(x => x.From.Id == userId).ToListAsync();
        }

        internal async Task<List<ContactRequest>> GetAllAsyncByToUserId(string userId)
        {
            return await _context.ContactRequest.Where(x => x.To.Id == userId).ToListAsync();
        }
    }
}
