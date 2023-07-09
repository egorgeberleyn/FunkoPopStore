using KittyStore.Domain.UserAggregate;

namespace KittyStore.Application.Common.Interfaces.Persistence
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllUsers();
        Task<User?> GetUserByIdAsync(Guid id);
        Task<User?> GetUserByEmailAsync(string email);
    
        Task AddUserAsync(User newUser);
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(User user);
    }
}