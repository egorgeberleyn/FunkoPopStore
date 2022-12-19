using System.Security.Cryptography;
using System.Text;
using KittyStore.Domain.CatAggregate;
using KittyStore.Domain.CatAggregate.Enums;
using KittyStore.Domain.CatAggregate.ValueObjects;
using KittyStore.Domain.UserAggregate;
using KittyStore.Domain.UserAggregate.Enums;
using KittyStore.Domain.UserAggregate.ValueObjects;

namespace KittyStore.Tests.Common;

public static class TestData
{
    //Test set of cats
    public static readonly List<Cat> Cats = new()
    {
        new Cat(KittyContextFactory.CatIdForDelete,
            "Lia", 5, "black", "britain", 70, CatGender.Female),
        new Cat(KittyContextFactory.CatIdForUpdate, 
            "Gail", 11, "white", "maine coon", 50, CatGender.Male),
        new Cat(CatId.CreateUnique(), 
            "God", 111, "rainbow", "king", 777, CatGender.Genderqueer),
    };
    
    public static List<User> Users => CreateUsers();
    
    private static List<User> CreateUsers()
    {
        CreateTestPasswordHash("111222333", out byte[] passwordHash1, out byte[] passwordSalt1);
        CreateTestPasswordHash("just777", out byte[] passwordHash2, out byte[] passwordSalt2);


        var users = new List<User>
        {
            new User(KittyContextFactory.UserAId ,"Pit", "Pot", "yuppi@gmail.com", 
                passwordHash1, passwordSalt1,
                Balance.Create(Currency.Euro, 228), Role.Customer, 
                DateTime.UtcNow, DateTime.UtcNow), // Test1

            new User(KittyContextFactory.UserBId,"Crock", "Tester", "ggwp@yandex.ru", 
                passwordHash2, passwordSalt2,
                Balance.Create(Currency.Dollar, 1337), Role.Customer,
                DateTime.UtcNow, DateTime.UtcNow), // Test2
        };

        return users;
    }
    
    private static void CreateTestPasswordHash(string password, out byte[] hash, out byte[] salt)
    {
        using var hmac = new HMACSHA512();
        salt = hmac.Key;
        hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
    }
}




