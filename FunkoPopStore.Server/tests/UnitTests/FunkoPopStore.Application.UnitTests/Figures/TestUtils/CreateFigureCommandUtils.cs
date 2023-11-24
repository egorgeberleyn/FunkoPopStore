using FunkoPopStore.Application.Figures.Commands.CreateFigure;
using FunkoPopStore.Application.UnitTests.TestUtils.Constants;

namespace FunkoPopStore.Application.UnitTests.Figures.TestUtils;

public class CreateFigureCommandUtils
{
    public static CreateFigureCommand CreateCommand(string? name = null, decimal? price = null, string? rarity = null, 
        Guid? seriesId = null, DateTime? productionOfYear = null) =>
        new(
            name ?? Constants.Figure.Name,
            seriesId ?? Constants.Figure.SeriesId,
            price ?? Constants.Figure.Price,
            rarity ?? Constants.Figure.Rarity,
            productionOfYear ?? Constants.Figure.ProductionOfYear);
}