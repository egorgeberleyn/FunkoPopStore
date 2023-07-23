using FluentAssertions;
using KittyStore.Application.Cats.Commands.DeleteCat;
using KittyStore.Application.Common.Interfaces.Persistence;
using KittyStore.Application.UnitTests.TestUtils.Constants;
using KittyStore.Domain.CatAggregate;
using KittyStore.Domain.CatAggregate.Enums;
using KittyStore.Domain.Common.Errors;
using Moq;

namespace KittyStore.Application.UnitTests.Cats.Commands.DeleteCat;

public class DeleteCatCommandHandlerTests
{
    private readonly Mock<ICatRepository> _mockCatRepository;
    private readonly DeleteCatCommandHandler _handler;

    public DeleteCatCommandHandlerTests()
    {
        _mockCatRepository = new Mock<ICatRepository>();
        _handler = new DeleteCatCommandHandler(_mockCatRepository.Object);
    }
    
    [Fact]
    public async Task HandleDeleteCatCommand_WhenCatIsValid_ShouldDeleteCat()
    {
        // Arrange
        var cat = Cat.Create(
            Constants.Cat.Name, Constants.Cat.Age, Constants.Cat.Color, 
            Constants.Cat.Breed, Constants.Cat.Price, CatGender.Male);
        
        _mockCatRepository.Setup(x => x.GetCatByIdAsync(cat.Id))
            .ReturnsAsync(cat);
        var deleteCatCommand = new DeleteCatCommand(cat.Id);
        
        // Act
        var result = await _handler.Handle(deleteCatCommand, default);

        // Assert
        result.IsError.Should().BeFalse();
        _mockCatRepository.Verify(m => m.DeleteCat(cat), Times.Once);
    }
    
    [Fact]
    public async Task HandleDeleteCatCommand_WhenCatIsNotFound_ShouldReturnNotFoundError()
    {
        // Arrange
        var incorrectId = Guid.NewGuid();

        _mockCatRepository.Setup(x => x.GetCatByIdAsync(incorrectId))
            .ReturnsAsync((Cat?)null);
        var deleteCatCommand = new DeleteCatCommand(incorrectId);
        
        // Act
        var result = await _handler.Handle(deleteCatCommand, default);

        // Assert
        result.IsError.Should().BeTrue();
        result.FirstError.Should().Be(Errors.Cat.NotFound);
    }
}