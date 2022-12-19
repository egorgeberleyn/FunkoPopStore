using KittyStore.Domain.CatAggregate.ValueObjects;
using KittyStore.Domain.UserAggregate.ValueObjects;
using KittyStore.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace KittyStore.Tests.Common;

public static class KittyContextFactory
{
    public static readonly UserId UserAId = UserId.CreateUnique();
    public static readonly UserId UserBId = UserId.CreateUnique();

    public static readonly CatId CatIdForDelete = CatId.CreateUnique();
    public static readonly CatId CatIdForUpdate = CatId.CreateUnique();
    
    public static AppDbContext Create()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        var context = new AppDbContext(options);
        context.Database.EnsureCreated();
        context.Users.AddRange(TestData.Users);
        context.Cats.AddRange(TestData.Cats);
        context.SaveChanges();

        return context;
    }

    public static void Destroy(AppDbContext context)
    {
        context.Database.EnsureDeleted();
        context.Dispose();
    }
}