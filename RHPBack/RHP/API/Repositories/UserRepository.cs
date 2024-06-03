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
            User? user =  await _context.Users.FirstOrDefaultAsync(p => p.Id == userId);

            if (user is null)
            {
                throw new Exception("User not found");
            }

            return user;
        }

        public User? GetUserByEmail(string email)
        {
            return _context.Users.FirstOrDefault(p => p.Email == email);
        }

        public async Task<List<ContactDTO>> GetContacts(string userId)
        {
            return await _context.Users
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
                    lastLogin = u.lastLogin!.Value
                })
                .ToListAsync();
        }

        //public async Task<Contact?> GetContact(string id)
        //{
        //    return await _context.Users
        //        .Where(u => u.Id == id)
        //        .Select(u => new ContactDTO
        //        {
        //            UserId = u.Id,
        //            Email = u.Email,
        //            Status = u.Status,
        //            loggedIn = u.loggedIn,
        //            lastLogin = u.lastLogin!.Value
        //        })
        //        .FirstOrDefaultAsync();
        //}
        internal async Task CreateUser(User user)
        {
            _context.Users.Add(user);
            await SaveChangesAsync();
        }

        internal async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        internal void Update(User user)
        {
           _context.Users.Update(user);
        }

        internal async Task<bool> IsUserLoggedIn(string userId)
        {
            return await _context.Users
                .Where(u => u.Id == userId)
                .Select(u => u.loggedIn)
                .FirstOrDefaultAsync();
        }
    }
}
