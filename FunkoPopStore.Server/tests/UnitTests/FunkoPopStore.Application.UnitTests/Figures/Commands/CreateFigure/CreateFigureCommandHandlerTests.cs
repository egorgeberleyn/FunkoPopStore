using FluentAssertions;
using FunkoPopStore.Application.Common.Interfaces.Persistence;
using FunkoPopStore.Application.Figures.Commands.CreateFigure;
using FunkoPopStore.Application.UnitTests.Figures.TestUtils;
using FunkoPopStore.Application.UnitTests.TestUtils.Figures.Extensions;
using Moq;

namespace FunkoPopStore.Application.UnitTests.Figures.Commands.CreateFigure;

public class CreateFigureCommandHandlerTests
{
    private readonly Mock<IFigureRepository> _mockFigureRepository;
    private readonly CreateFigureCommandHandler _handler;

    public CreateFigureCommandHandlerTests()
    {
        _mockFigureRepository = new Mock<IFigureRepository>();
        _handler = new CreateFigureCommandHandler(_mockFigureRepository.Object);
    }

    // test naming
    // T1: SUT - logical component we're testing
    // T2: Scenario - what we're testing
    // T3: Expected outcome - what we expect the logical component to do
    [Theory]
    [MemberData(nameof(ValidCreateFigureCommands))]
    public async Task HandleCreateFigureCommand_WhenIsValid_ShouldCreateAndReturn(
        CreateFigureCommand createFigureCommand)
    {
        // Act
        var result = await _handler.Handle(createFigureCommand, default);

        // Assert
        // 1. Validate correct cat created based on command
        result.IsError.Should().BeFalse();
        result.Value.ValidateCreatedFrom(createFigureCommand);
        // 2. Figure added to repository
        _mockFigureRepository.Verify(m => m.CreateFigureAsync(result.Value), Times.Once);
        _mockFigureRepository.VerifyNoOtherCalls();
    }

    public static IEnumerable<object[]> ValidCreateFigureCommands()
    {
        yield return new object[] { CreateFigureCommandUtils.CreateCommand() };
        yield return new object[]
        {
            CreateFigureCommandUtils.CreateCommand(
                price: 200, rarity: "Common")
        };
    }
}