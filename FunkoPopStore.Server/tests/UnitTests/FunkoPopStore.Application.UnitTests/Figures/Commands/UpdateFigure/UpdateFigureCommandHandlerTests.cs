using FluentAssertions;
using FunkoPopStore.Application.Common.Interfaces.Persistence;
using FunkoPopStore.Application.Figures.Commands.UpdateFigure;
using FunkoPopStore.Application.UnitTests.TestUtils.Constants;
using FunkoPopStore.Application.UnitTests.TestUtils.Figures.Extensions;
using FunkoPopStore.Domain.Common.Errors;
using FunkoPopStore.Domain.FigureAggregate;
using FunkoPopStore.Domain.FigureAggregate.Enums;
using Moq;

namespace FunkoPopStore.Application.UnitTests.Figures.Commands.UpdateFigure;

public class UpdateFigureCommandHandlerTests
{
    private readonly Mock<IFigureRepository> _mockFigureRepository;
    private readonly UpdateFigureCommandHandler _handler;

    public UpdateFigureCommandHandlerTests()
    {
        _mockFigureRepository = new Mock<IFigureRepository>();
        _handler = new UpdateFigureCommandHandler(_mockFigureRepository.Object);
    }

    [Fact]
    public async Task HandleUpdateFigureCommand_WhenIsValid_ShouldUpdateAndReturn()
    {
        // Arrange
        var figure = Figure.Create(
            Constants.Figure.Name, Constants.Figure.Price, Rarity.Common, 
            Constants.Figure.ProductionOfYear, Constants.Figure.SeriesId);
        
        const string updatedName = "Willy Jr";
        const decimal updatedPrice = 240;

        _mockFigureRepository.Setup(x => x.GetFigureByIdAsync(figure.Id))
            .ReturnsAsync(figure);
        var updateFigureCommand = new UpdateFigureCommand(figure.Id, updatedName, figure.SeriesId,
            updatedPrice, figure.Rarity.ToString());

        // Act
        var result = await _handler.Handle(updateFigureCommand, default);

        // Assert
        result.IsError.Should().BeFalse();
        result.Value.ValidateUpdatedFrom(updateFigureCommand);
        _mockFigureRepository.Verify(m => m.UpdateFigure(result.Value), Times.Once);
    }

    [Fact]
    public async Task HandleUpdateFigureCommand_WhenIsNotFound_ShouldReturnNotFoundError()
    {
        // Arrange
        var figure = Figure.Create(
            Constants.Figure.Name, Constants.Figure.Price, Rarity.Common, 
            Constants.Figure.ProductionOfYear, Constants.Figure.SeriesId);

        const string updatedName = "Willy Jr";
        const decimal updatedPrice = 240;
        
        _mockFigureRepository.Setup(x => x.GetFigureByIdAsync(Constants.Figure.IncorrectId))
            .ReturnsAsync((Figure?)null);
        var updateFigureCommand = new UpdateFigureCommand(Constants.Figure.IncorrectId, updatedName, figure.SeriesId,
            updatedPrice, figure.Rarity.ToString());

        // Act
        var result = await _handler.Handle(updateFigureCommand, default);

        // Assert
        result.IsError.Should().BeTrue();
        result.FirstError.Should().Be(Errors.Figure.NotFound);
        _mockFigureRepository.Verify(m => m.UpdateFigure(result.Value), Times.Never);
    }
}