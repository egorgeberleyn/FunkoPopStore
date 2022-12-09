using KittyStore.Application.Common.Interfaces.Persistence;
using KittyStore.Domain.UserAggregate;
using Microsoft.EntityFrameworkCore;

namespace KittyStore.Infrastructure.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetUserByEmailAsync(string email) =>
        await _context.Users.SingleOrDefaultAsync(u => u.Email == email);

    public async Task AddUserAsync(User newUser)
    {
        await _context.AddAsync(newUser);
        await _context.SaveChangesAsync();
    }
        
}