using KittyStore.Application.Common.Interfaces.Persistence;
using KittyStore.Domain.CatAggregate;
using KittyStore.Domain.CatAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace KittyStore.Infrastructure.Persistence.Repositories;

public class CatRepository : ICatRepository
{
    private readonly AppDbContext _context;

    public CatRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Cat>> GetAllCatsAsync() =>
        await _context.Cats.ToListAsync();

    public async Task<Cat?> GetCatByIdAsync(CatId id) =>
        await _context.Cats.FirstOrDefaultAsync(c => c.Id == id);
    
    public async Task CreateCatAsync(Cat newCat)
    {
        await _context.AddAsync(newCat);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateCatAsync(Cat cat)
    {
        _context.Update(cat);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteCatAsync(Cat cat)
    {
        _context.Remove(cat);
        await _context.SaveChangesAsync();
    }
}