using KittyStore.Domain.CatAggregate;
using KittyStore.Domain.CatAggregate.ValueObjects;

namespace KittyStore.Application.Common.Interfaces.Persistence
{
    public interface ICatRepository
    {
        Task<List<Cat>> GetAllCatsAsync();
        Task<Cat?> GetCatByIdAsync(CatId id);
        Task CreateCatAsync(Cat newCat);
        Task UpdateCatAsync(Cat cat);
        Task DeleteCatAsync(Cat cat);
    
    }
}