using KittyStore.Domain.UserAggregate;

namespace KittyStore.Application.Common.Interfaces.Persistence;

public interface IUserRepository
{
    Task<User?> GetUserByEmailAsync(string email);
    Task AddUserAsync(User newUser);
}