namespace FunkoPopStore.Contracts.Admin.Figures;

public record CreateFigureRequest(
    string Name,
    decimal Price,
    string Rarity,
    Guid SeriesId,
    DateTime ProductionOfYear);