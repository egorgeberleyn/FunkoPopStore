using FluentAssertions;
using FunkoPopStore.Application.Figures.Commands.CreateFigure;
using FunkoPopStore.Application.Figures.Commands.UpdateFigure;
using FunkoPopStore.Domain.FigureAggregate;

namespace FunkoPopStore.Application.UnitTests.TestUtils.Figures.Extensions;

public static partial class FigureExtensions
{
    public static void ValidateCreatedFrom(this Figure figure, CreateFigureCommand command)
    {
        figure.Id.Should().NotBeEmpty();
        figure.Name.Should().Be(command.Name);
        figure.Price.Should().Be(command.Price);
        figure.Rarity.ToString().Should().Be(command.Rarity);
        figure.ProductionYear.Should().Be(command.ProductionYear);
        figure.SeriesId.Should().Be(command.SeriesId);
    }

    public static void ValidateUpdatedFrom(this Figure figure, UpdateFigureCommand command)
    {
        figure.Id.Should().NotBeEmpty();
        figure.Id.Should().Be(command.Id);
        figure.SeriesId.Should().NotBeEmpty();
        figure.SeriesId.Should().Be(command.SeriesId);
        figure.Name.Should().Be(command.Name);
        figure.Price.Should().Be(command.Price);
        figure.Rarity.ToString().Should().Be(command.Rarity);
    }
}