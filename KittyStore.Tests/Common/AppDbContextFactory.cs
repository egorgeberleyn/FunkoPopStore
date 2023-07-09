using KittyStore.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace KittyStore.Tests.Common;

public static class AppDbContextFactory
{
    public static readonly Guid UserAId = Guid.NewGuid();
    public static readonly Guid UserBId = Guid.NewGuid();

    public static readonly Guid CatIdForDelete = Guid.NewGuid();
    public static readonly Guid CatIdForUpdate = Guid.NewGuid();
    
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