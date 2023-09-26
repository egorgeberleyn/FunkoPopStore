using KittyStore.Domain.UserAggregate;

namespace KittyStore.Application.Common.Interfaces.Persistence
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllUsers();
        Task<User?> GetUserByIdAsync(Guid id);
        Task<User?> GetUserByEmailAsync(string email);

        Task AddUserAsync(User newUser);
        void UpdateUser(User user);
        void DeleteUser(User user);
    }
}