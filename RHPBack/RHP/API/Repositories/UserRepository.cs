﻿using RHP.Data;
using RHP.Entities.Models;

namespace RHP.API.Repositories
{
    public class UserRepository : GenericRepository<User>
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public User? GetByUsername(string email)
        {
            return _context.User.FirstOrDefault(p => p.Email == email);
        }
    }
}