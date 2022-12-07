using KittyStore.Application.Common.Interfaces.Persistence;
using KittyStore.Domain.UserAggregate;

namespace KittyStore.Infrastructure.Persistence;

public class UserRepository : IUserRepository
{
    private static readonly List<User> Users = new();
    
    public User? GetUserByEmail(string email) =>
        Users.SingleOrDefault(u => u.Email == email);
    
    public void Add(User newUser) =>
        Users.Add(newUser);
}