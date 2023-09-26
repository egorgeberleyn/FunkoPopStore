using KittyStore.Application.Common.Interfaces.Persistence;
using KittyStore.Domain.CatAggregate;
using Microsoft.EntityFrameworkCore;

namespace KittyStore.Infrastructure.Persistence.Repositories
{
    public class CatRepository : ICatRepository
    {
        private readonly AppDbContext _context;

        public CatRepository(AppDbContext context)
        {
            _context = context;
        }

        public Task<List<Cat>> GetAllCatsAsync() =>
            _context.Cats.ToListAsync();

        public Task<Cat?> GetCatByIdAsync(Guid id) =>
            _context.Cats.FirstOrDefaultAsync(c => c.Id == id);

        public async Task CreateCatAsync(Cat newCat) =>
            await _context.AddAsync(newCat);

        public void UpdateCat(Cat cat) =>
            _context.Update(cat);

        public void DeleteCat(Cat cat) =>
            _context.Remove(cat);
    }
}