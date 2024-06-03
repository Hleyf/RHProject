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
