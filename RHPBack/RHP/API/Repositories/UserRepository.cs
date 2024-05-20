using Microsoft.EntityFrameworkCore;
using RHP.Data;
using RHP.Entities.Models;
using RHP.Entities.Models.DTOs;

namespace RHP.API.Repositories
{
    public class UserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public User? GetUserByEmail(string email)
        {
            return _context.User.FirstOrDefault(p => p.Email == email);
        }

        public async Task<List<ContactDTO>> GetContacts(string userId)
        {
            return await _context.User
                .Where(u => u.Id == userId)
                .SelectMany(u => u.Contacts)
                .OrderByDescending(u => u.Status == UserStatus.Online)
                .ThenByDescending(u => u.lastLogin)
                .Select(u => new ContactDTO
                {
                    UserId = u.Id,
                    Email = u.Email,
                    Status = u.Status,
                    loggedIn = u.loggedIn,
                    lastLoggedIn = u.lastLogin.Value
                })
                .ToListAsync();
        }

        internal async Task<ContactDTO?> GetContact(string id)
        {
            return await _context.User
                .Where(u => u.Id == id)
                .Select(u => new ContactDTO
                {
                    UserId = u.Id,
                    Email = u.Email,
                    Status = u.Status,
                    loggedIn = u.loggedIn,
                    lastLoggedIn = u.lastLogin!.Value
                })
                .FirstOrDefaultAsync();
        }
    }
}
