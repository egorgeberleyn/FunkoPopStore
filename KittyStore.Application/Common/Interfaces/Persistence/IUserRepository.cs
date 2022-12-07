using KittyStore.Domain.UserAggregate;

namespace KittyStore.Application.Common.Interfaces.Persistence;

public interface IUserRepository
{
    User? GetUserByEmail(string email);
    void Add(User newUser);
}