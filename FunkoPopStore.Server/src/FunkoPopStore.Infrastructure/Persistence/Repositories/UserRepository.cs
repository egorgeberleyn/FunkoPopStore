using FunkoPopStore.Application.Common.Interfaces.Persistence;
using FunkoPopStore.Domain.UserAggregate;
using Microsoft.EntityFrameworkCore;

namespace FunkoPopStore.Infrastructure.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public Task<List<User>> GetAllUsers() =>
        _context.Users.ToListAsync();

    public Task<User?> GetUserByIdAsync(Guid id) =>
        _context.Users.FirstOrDefaultAsync(u => u.Id == id);

    public Task<User?> GetUserByEmailAsync(string email) =>
        _context.Users.SingleOrDefaultAsync(u => u.Email == email);

    public async Task AddUserAsync(User newUser) =>
        await _context.AddAsync(newUser);

    public void UpdateUser(User user) =>
        _context.Update(user);

    public void DeleteUser(User user) =>
        _context.Remove(user);
}