using KittyStore.Infrastructure.Persistence;
using KittyStore.Infrastructure.Persistence.Repositories;

namespace KittyStore.Tests.Common;

public class TestCommandBase : IDisposable
{
    protected readonly AppDbContext Context;
    protected readonly CatRepository CatRepository;
    protected readonly UserRepository UserRepository;

    public TestCommandBase()
    {
        Context = KittyContextFactory.Create();
        CatRepository = new CatRepository(Context);
        UserRepository = new UserRepository(Context);
    }

    public void Dispose()
    {
        KittyContextFactory.Destroy(Context);
    }
}