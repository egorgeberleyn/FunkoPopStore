﻿using KittyStore.Application.Common.Interfaces.Persistence;
using KittyStore.Domain.UserAggregate;
using Microsoft.EntityFrameworkCore;

namespace KittyStore.Infrastructure.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<User>> GetAllUsers() =>
            await _context.Users.ToListAsync();
        
        public async Task<User?> GetUserByIdAsync(Guid id) =>
            await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
    
        public async Task<User?> GetUserByEmailAsync(string email) =>
            await _context.Users.SingleOrDefaultAsync(u => u.Email == email);

        public async Task AddUserAsync(User newUser)
        {
            await _context.AddAsync(newUser);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUserAsync(User user)
        {
            _context.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(User user)
        {
            _context.Remove(user);
            await _context.SaveChangesAsync();
        }
    }
}