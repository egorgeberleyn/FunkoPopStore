using FunkoPopStore.Domain.FigureAggregate;

namespace FunkoPopStore.Application.Common.Interfaces.Persistence;

public interface IFigureRepository
{
    Task<List<Figure>> GetAllFiguresAsync();
    Task<Figure?> GetFigureByIdAsync(Guid id);
    Task CreateFigureAsync(Figure newFigure);
    void UpdateFigure(Figure figure);
    void DeleteFigure(Figure figure);
}