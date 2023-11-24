namespace FunkoPopStore.Contracts.Admin.Figures;

public record FigureResponse(
    Guid Id,
    string Name,
    decimal Price,
    string Rarity,
    Guid SeriesId,
    DateTime ProductionOfYear);