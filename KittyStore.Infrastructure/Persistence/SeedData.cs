using System.Security.Cryptography;
using System.Text;
using KittyStore.Application.Common.Interfaces.Utils;
using KittyStore.Domain.CatAggregate;
using KittyStore.Domain.CatAggregate.Enums;
using KittyStore.Domain.UserAggregate;
using KittyStore.Domain.UserAggregate.Enums;
using KittyStore.Domain.UserAggregate.ValueObjects;

namespace KittyStore.Infrastructure.Persistence;

public class SeedData
{
    public static List<Cat> Cats => new()
    {
        Cat.Create("Lia", 5, "black", "britain", 70, CatGender.Female),
        Cat.Create("Gail", 11, "white", "maine coon", 50, CatGender.Male),
        Cat.Create("Niggas", 3, "ginger", "egypt", 20, CatGender.Male),
        Cat.Create("Sunny", 6, "gray", "german", 88, CatGender.Female),
    };
    
    public static User CreateAdmin(string firstName, string lastName, string password, string email)
    {
        var salt = RandomNumberGenerator.GetBytes(64);;
        var hash = Rfc2898DeriveBytes.Pbkdf2(
            Encoding.UTF8.GetBytes(password), salt, 350000, HashAlgorithmName.SHA512, 64);
        return User.Create(firstName, lastName, email, hash,
            salt, Balance.Create(Currency.Dollar, 1000), Role.Admin);
    }
}