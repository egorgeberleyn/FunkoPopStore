

using FluentAssertions;
using KittyStore.Application.Cats.Commands.CreateCat;
using KittyStore.Application.Common.Interfaces.Persistence;
using KittyStore.Application.UnitTests.Cats.TestUtils;
using KittyStore.Application.UnitTests.TestUtils.Cats.Extensions;
using Moq;

namespace KittyStore.Application.UnitTests.Cats.Commands.CreateCat;

public class CreateCatCommandHandlerTests
{
    private readonly Mock<ICatRepository> _mockCatRepository;
    private readonly CreateCatCommandHandler _handler;

    public CreateCatCommandHandlerTests()
    {
        _mockCatRepository = new Mock<ICatRepository>();
        _handler = new CreateCatCommandHandler(_mockCatRepository.Object);
    }

    // test naming
    // T1: SUT - logical component we're testing
    // T2: Scenario - what we're testing
    // T3: Expected outcome - what we expect the logical component to do
    [Theory]
    [MemberData(nameof(ValidCreateMenuCommands))]
    public async Task HandleCreateCatCommand_WhenCatIsValid_ShouldCreateAndReturnCat(CreateCatCommand createCatCommand)
    {
        // Act
        var result = await _handler.Handle(createCatCommand, default);

        // Assert
        // 1. Validate correct cat created based on command
        result.IsError.Should().BeFalse();
        result.Value.ValidateCreatedFrom(createCatCommand);
        // 2. Cat added to repository
        _mockCatRepository.Verify(m => m.CreateCatAsync(result.Value));
    }

    public static IEnumerable<object[]> ValidCreateMenuCommands()
    {
        yield return new object[] { CreateCatCommandUtils.CreateCommand() };
        yield return new object[] { CreateCatCommandUtils.CreateCommand(
            price: 200, gender: "Female") };
    }
}