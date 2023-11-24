using FluentAssertions;
using FunkoPopStore.Application.Common.Interfaces.Persistence;
using FunkoPopStore.Application.Figures.Commands.DeleteFigure;
using FunkoPopStore.Application.UnitTests.TestUtils.Constants;
using FunkoPopStore.Domain.Common.Errors;
using FunkoPopStore.Domain.FigureAggregate;
using FunkoPopStore.Domain.FigureAggregate.Enums;
using Moq;

namespace FunkoPopStore.Application.UnitTests.Figures.Commands.DeleteFigure;

public class DeleteFigureCommandHandlerTests
{
    private readonly Mock<IFigureRepository> _mockFigureRepository;
    private readonly DeleteFigureCommandHandler _handler;

    public DeleteFigureCommandHandlerTests()
    {
        _mockFigureRepository = new Mock<IFigureRepository>();
        _handler = new DeleteFigureCommandHandler(_mockFigureRepository.Object);
    }

    [Fact]
    public async Task HandleDeleteFigureCommand_WhenIsValid_ShouldDelete()
    {
        // Arrange
        var figure = Figure.Create(
            Constants.Figure.Name, Constants.Figure.Price, Rarity.Common, 
            Constants.Figure.ProductionOfYear, Constants.Figure.SeriesId);

        _mockFigureRepository.Setup(x => x.GetFigureByIdAsync(figure.Id))
            .ReturnsAsync(figure);
        var deleteFigureCommand = new DeleteFigureCommand(figure.Id);

        // Act
        var result = await _handler.Handle(deleteFigureCommand, default);

        // Assert
        result.IsError.Should().BeFalse();
        _mockFigureRepository.Verify(m => m.DeleteFigure(figure), Times.Once);
    }

    [Fact]
    public async Task HandleDeleteFigureCommand_WhenIsNotFound_ShouldReturnNotFoundError()
    {
        // Arrange
        _mockFigureRepository.Setup(x => x.GetFigureByIdAsync(Constants.Figure.IncorrectId))
            .ReturnsAsync((Figure?)null);
        var deleteFigureCommand = new DeleteFigureCommand(Constants.Figure.IncorrectId);

        // Act
        var result = await _handler.Handle(deleteFigureCommand, default);

        // Assert
        result.IsError.Should().BeTrue();
        result.FirstError.Should().Be(Errors.Figure.NotFound);
        _mockFigureRepository.Verify(x => x.DeleteFigure(It.IsAny<Figure>()), Times.Never);
    }
}