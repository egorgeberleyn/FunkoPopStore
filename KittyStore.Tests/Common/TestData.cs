using KittyStore.Domain.CatAggregate;
using KittyStore.Domain.CatAggregate.Enums;
using KittyStore.Domain.UserAggregate;
using KittyStore.Domain.UserAggregate.Enums;
using KittyStore.Domain.UserAggregate.ValueObjects;

namespace KittyStore.Tests.Common;

public static class TestData
{
    //Test set of cats
    public static readonly List<Cat> Cats = new()
    {
        new Cat(AppDbContextFactory.CatIdForDelete,
            "Lia", 5, "black", "britain", 70, CatGender.Female),
        new Cat(AppDbContextFactory.CatIdForUpdate,
            "Gail", 11, "white", "maine coon", 50, CatGender.Male),
        new Cat(Guid.NewGuid(), 
            "God", 111, "rainbow", "king", 777, CatGender.Genderqueer),
    };

    //Test set of users
    public static readonly List<User> Users = new()
    {
        new User(AppDbContextFactory.UserAId, "Pit", "Pot", "yuppi@gmail.com",
            new byte[] { 1, 2, 3 }, new byte[] { 4, 5, 6 },
            Balance.Create(Currency.Euro, 228), Role.Customer,
            DateTime.UtcNow, DateTime.UtcNow), // Test1

        new User(AppDbContextFactory.UserBId, "Crock", "Tester", "ggwp@yandex.ru",
            new byte[] { 1, 2, 3 }, new byte[] { 4, 5, 6 },
            Balance.Create(Currency.Dollar, 1337), Role.Customer,
            DateTime.UtcNow, DateTime.UtcNow), // Test2
    };
}