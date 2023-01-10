using KittyStore.Infrastructure.Persistence;
using KittyStore.Infrastructure.Persistence.Repositories;

namespace KittyStore.Tests.Common;

public class TestCommandBase : IDisposable
{
    protected readonly AppDbContext Context;
    protected readonly CatRepository CatRepository;
    protected readonly UserRepository UserRepository;

    protected TestCommandBase()
    {
        Context = AppDbContextFactory.Create();
        CatRepository = new CatRepository(Context);
        UserRepository = new UserRepository(Context);
    }

    public void Dispose()
    {
        AppDbContextFactory.Destroy(Context);
    }
}