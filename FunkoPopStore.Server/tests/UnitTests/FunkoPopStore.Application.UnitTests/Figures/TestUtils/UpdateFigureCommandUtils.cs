using FunkoPopStore.Application.Figures.Commands.UpdateFigure;
using FunkoPopStore.Application.UnitTests.TestUtils.Constants;

namespace FunkoPopStore.Application.UnitTests.Figures.TestUtils;

public class UpdateFigureCommandUtils
{
    public static UpdateFigureCommand CreateCommand(Guid? id = null, string? name = null, decimal? price = null, 
        string? rarity = null, Guid? seriesId = null) =>
        new(id ?? Constants.Figure.Id,
            name ?? Constants.Figure.Name,
            seriesId ?? Constants.Figure.SeriesId,
            price ?? Constants.Figure.Price,
            rarity ?? Constants.Figure.Rarity
            );
}