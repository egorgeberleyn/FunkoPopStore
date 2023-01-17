using System.Security.Cryptography;
using System.Text;
using KittyStore.Domain.CatAggregate;
using KittyStore.Domain.CatAggregate.Enums;
using KittyStore.Domain.UserAggregate;
using KittyStore.Domain.UserAggregate.Enums;
using KittyStore.Domain.UserAggregate.ValueObjects;

namespace KittyStore.Infrastructure.Persistence;

internal static class SeedData
{
    private static (byte[] Hash, byte[] Salt) AdminPasswordInfo => CreateTestPasswordHash("secret123");
    private static (byte[] Hash, byte[] Salt) CustomerPasswordInfo => CreateTestPasswordHash("simple");
    
    public static List<User> Users => new List<User>()
    {
        // Admin (password = secret123)
        User.Create("Jorge", "Admin", "admin123@gmail.com", AdminPasswordInfo.Hash, 
            AdminPasswordInfo.Salt, Balance.Create(Currency.Dollar, 1000), Role.Admin),

        // Test customer (password = simple)
        User.Create("Don", "Test Customer", "1v2goog@gmail.com", CustomerPasswordInfo.Hash, 
            CustomerPasswordInfo.Salt, Balance.Create(Currency.Dollar, 500), Role.Customer),
    };

    public static List<Cat> Cats => new List<Cat>
    {
        Cat.Create("Lia", 5, "black", "britain", 70, CatGender.Female),
        Cat.Create("Gail", 11, "white", "maine coon", 50, CatGender.Male),
        Cat.Create("Neegus", 3, "ginger", "egypt", 20, CatGender.Male),
        Cat.Create("Sunny", 6, "gray", "german", 88, CatGender.Female),
    };
    
    private static (byte[] Hash, byte[] Salt) CreateTestPasswordHash(string password)
    {
        using var hmac = new HMACSHA512();
        var salt = hmac.Key;
        var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        return (hash, salt);
    }
}