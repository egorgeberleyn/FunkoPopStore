using FluentAssertions;
using KittyStore.Application.Cats.Commands.UpdateCat;
using KittyStore.Application.Common.Interfaces.Persistence;
using KittyStore.Application.UnitTests.TestUtils.Cats.Extensions;
using KittyStore.Application.UnitTests.TestUtils.Constants;
using KittyStore.Domain.CatAggregate;
using KittyStore.Domain.CatAggregate.Enums;
using KittyStore.Domain.Common.Errors;
using Moq;

namespace KittyStore.Application.UnitTests.Cats.Commands.UpdateCat;

public class UpdateCatCommandHandlerTests
{
    private readonly Mock<ICatRepository> _mockCatRepository;
    private readonly UpdateCatCommandHandler _handler;

    public UpdateCatCommandHandlerTests()
    {
        _mockCatRepository = new Mock<ICatRepository>();
        _handler = new UpdateCatCommandHandler(_mockCatRepository.Object);
    }

    [Fact]
    public async Task HandleUpdateCatCommand_WhenCatIsValid_ShouldUpdateAndReturnCat()
    {
        // Arrange
        var cat = Cat.Create(
            Constants.Cat.Name, Constants.Cat.Age, Constants.Cat.Color,
            Constants.Cat.Breed, Constants.Cat.Price, CatGender.Male);

        _mockCatRepository.Setup(x => x.GetCatByIdAsync(cat.Id))
            .ReturnsAsync(cat);
        var updateCatCommand = new UpdateCatCommand(cat.Id, "Willy Jr", cat.Age, cat.Color, cat.Breed,
            cat.Gender.ToString(), 240);

        // Act
        var result = await _handler.Handle(updateCatCommand, default);

        // Assert
        result.IsError.Should().BeFalse();
        result.Value.ValidateUpdatedFrom(updateCatCommand);
        _mockCatRepository.Verify(m => m.UpdateCat(result.Value), Times.Once);
    }

    [Fact]
    public async Task HandleUpdateCatCommand_WhenCatIsNotFound_ShouldReturnNotFoundError()
    {
        // Arrange
        var cat = Cat.Create(
            Constants.Cat.Name, Constants.Cat.Age, Constants.Cat.Color,
            Constants.Cat.Breed, Constants.Cat.Price, CatGender.Male);

        _mockCatRepository.Setup(x => x.GetCatByIdAsync(Constants.Cat.IncorrectId))
            .ReturnsAsync((Cat?)null);
        var updateCatCommand = new UpdateCatCommand(Constants.Cat.IncorrectId, "Willy Jr", cat.Age, cat.Color,
            cat.Breed, cat.Gender.ToString(), 240);

        // Act
        var result = await _handler.Handle(updateCatCommand, default);

        // Assert
        result.IsError.Should().BeTrue();
        result.FirstError.Should().Be(Errors.Cat.NotFound);
        _mockCatRepository.Verify(m => m.UpdateCat(result.Value), Times.Never);
    }
}