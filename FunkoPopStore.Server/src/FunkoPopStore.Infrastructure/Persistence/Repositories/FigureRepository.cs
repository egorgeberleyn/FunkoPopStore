using FunkoPopStore.Application.Common.Interfaces.Persistence;
using FunkoPopStore.Domain.FigureAggregate;
using Microsoft.EntityFrameworkCore;

namespace FunkoPopStore.Infrastructure.Persistence.Repositories;

public class FigureRepository : IFigureRepository
{
    private readonly AppDbContext _context;

    public FigureRepository(AppDbContext context)
    {
        _context = context;
    }

    public Task<List<Figure>> GetAllFiguresAsync() =>
        _context.Figures.ToListAsync();

    public Task<Figure?> GetFigureByIdAsync(Guid id) =>
        _context.Figures.FirstOrDefaultAsync(c => c.Id == id);

    public async Task CreateFigureAsync(Figure newFigure) =>
        await _context.AddAsync(newFigure);

    public void UpdateFigure(Figure figure) =>
        _context.Update(figure);

    public void DeleteFigure(Figure figure) =>
        _context.Remove(figure);
}