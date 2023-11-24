namespace FunkoPopStore.Contracts.Admin.Figures;

public record UpdateFigureRequest(
    string Name,
    decimal Price,
    string Rarity,
    Guid SeriesId);