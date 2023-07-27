using KittyStore.Application.UnitTests.TestUtils.Constants;
using KittyStore.Domain.UserAggregate;
using KittyStore.Domain.UserAggregate.Enums;
using KittyStore.Domain.UserAggregate.ValueObjects;

namespace KittyStore.Application.UnitTests.Users.UserUtils;

public class CreateUserUtils
{
    public static User CreateTestUser(string? firstName = null, string? lastName = null, string? email = null,
        Role? role = Role.Customer, Balance? balance = null)
    {
        return User.Create(
            firstName ?? Constants.User.FirstName,
            lastName ?? Constants.User.LastName,
            email ?? Constants.User.Email,
            Constants.User.PasswordHash,
            Constants.User.PasswordSalt,
            role ?? Role.Customer,
            balance ?? Constants.User.Balance);
    }
}