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

        public async Task<User> GetByUserId(string userId)
        {
            User? user =  await _context.User.FirstOrDefaultAsync(p => p.Id == userId);

            if (user is null)
            {
                throw new Exception("User not found");
            }

            return user;
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
                    lastLoggedIn = u.lastLogin!.Value
                })
                .ToListAsync();
        }

        public async Task<ContactDTO?> GetContact(string id)
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

        internal async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
