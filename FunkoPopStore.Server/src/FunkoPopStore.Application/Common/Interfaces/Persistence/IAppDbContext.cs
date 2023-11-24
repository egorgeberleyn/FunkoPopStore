namespace FunkoPopStore.Application.Common.Interfaces.Persistence;

public interface IAppDbContext
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}