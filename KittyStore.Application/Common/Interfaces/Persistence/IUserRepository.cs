using KittyStore.Domain.UserAggregate;
using KittyStore.Domain.UserAggregate.ValueObjects;

namespace KittyStore.Application.Common.Interfaces.Persistence;

public interface IUserRepository
{
    Task<User?> GetUserByIdAsync(UserId id);
    Task<User?> GetUserByEmailAsync(string email);
    
    Task AddUserAsync(User newUser);
    Task UpdateUserAsync(User user);
    Task DeleteUserAsync(User user);
}