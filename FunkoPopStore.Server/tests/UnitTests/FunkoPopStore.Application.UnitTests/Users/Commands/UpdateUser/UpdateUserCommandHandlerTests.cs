using FluentAssertions;
using FunkoPopStore.Application.Common.Interfaces.Persistence;
using FunkoPopStore.Application.UnitTests.TestUtils.Constants;
using FunkoPopStore.Application.UnitTests.Users.UserUtils;
using FunkoPopStore.Application.Users.Commands.UpdateUser;
using FunkoPopStore.Domain.Common.Errors;
using FunkoPopStore.Domain.UserAggregate;
using FunkoPopStore.Domain.UserAggregate.Enums;
using FunkoPopStore.Domain.UserAggregate.ValueObjects;
using Moq;

namespace FunkoPopStore.Application.UnitTests.Users.Commands.UpdateUser;

public class UpdateUserCommandHandlerTests
{
    private readonly Mock<IUserRepository> _mockUserRepository;
    private readonly UpdateUserCommandHandler _handler;

    public UpdateUserCommandHandlerTests()
    {
        _mockUserRepository = new Mock<IUserRepository>();
        _handler = new UpdateUserCommandHandler(_mockUserRepository.Object);
    }

    [Fact]
    public async Task Handle_UpdateUserCommand_WhenUserNotFound_ReturnNotFoundError()
    {
        //Arrange
        _mockUserRepository.Setup(x => x.GetUserByIdAsync(Constants.User.IncorrectId)).ReturnsAsync((User?)null);
        var updateUserCommand = new UpdateUserCommand(Constants.User.IncorrectId, Constants.User.FirstName,
            Constants.User.LastName, Constants.User.Email,
            new BalanceCommand(Constants.User.Balance.Currency.ToString(), Constants.User.Balance.Amount));

        //Act
        var result = await _handler.Handle(updateUserCommand, default);

        //Assert
        result.IsError.Should().BeTrue();
        result.FirstError.Should().Be(Errors.User.NotFound);
        _mockUserRepository.Verify(x => x.UpdateUser(It.IsAny<User>()), Times.Never);
    }

    [Theory]
    [MemberData(nameof(ValidUsers))]
    public async Task Handle_UpdateUserCommand_WhenUserIsValid_UpdateAndReturnUpdatedUser(User user)
    {
        //Arrange
        _mockUserRepository.Setup(x => x.GetUserByIdAsync(user.Id)).ReturnsAsync(user);

        const string updatedFirstName = "Pit";
        const string updatedLastName = "Bronson";
        var updatedBalance = Balance.Create(Currency.Dollar, 300);

        var updateUserCommand = new UpdateUserCommand(user.Id, updatedFirstName, updatedLastName, user.Email,
            new BalanceCommand(updatedBalance.Currency.ToString(), updatedBalance.Amount));

        //Act
        var result = await _handler.Handle(updateUserCommand, default);

        //Assert
        result.IsError.Should().BeFalse();
        result.Value.FirstName.Should().Be(updatedFirstName);
        result.Value.LastName.Should().Be(updatedLastName);
        result.Value.Balance.Should().Be(updatedBalance);
        _mockUserRepository.Verify(x => x.UpdateUser(user), Times.Once);
    }

    public static IEnumerable<object[]> ValidUsers()
    {
        yield return new object[] { CreateUserUtils.CreateTestUser(role: Role.Admin) };
        yield return new object[] { CreateUserUtils.CreateTestUser(balance: Balance.Create(Currency.Euro, 0)) };
        yield return new object[] { CreateUserUtils.CreateTestUser(firstName: "John", lastName: "Silver") };
    }
}