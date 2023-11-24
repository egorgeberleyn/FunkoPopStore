using FluentAssertions;
using FunkoPopStore.Application.Common.Interfaces.Persistence;
using FunkoPopStore.Application.UnitTests.TestUtils.Constants;
using FunkoPopStore.Application.UnitTests.Users.UserUtils;
using FunkoPopStore.Application.Users.Commands.DeleteUser;
using FunkoPopStore.Domain.Common.Errors;
using FunkoPopStore.Domain.UserAggregate;
using FunkoPopStore.Domain.UserAggregate.Enums;
using Moq;

namespace FunkoPopStore.Application.UnitTests.Users.Commands.DeleteUser;

public class DeleteUserHandlerTests
{
    private readonly Mock<IUserRepository> _mockUserRepository;
    private readonly DeleteUserCommandHandler _handler;

    public DeleteUserHandlerTests()
    {
        _mockUserRepository = new Mock<IUserRepository>();
        _handler = new DeleteUserCommandHandler(_mockUserRepository.Object);
    }

    [Fact]
    public async Task HandleDeleteCommand_WhenUserNotFound_ReturnNotFoundError()
    {
        //Arrange
        _mockUserRepository.Setup(x => x.GetUserByIdAsync(Constants.User.IncorrectId)).ReturnsAsync((User?)null);

        //Act
        var result = await _handler.Handle(new DeleteUserCommand(Constants.User.IncorrectId), default);

        //Assert
        result.IsError.Should().BeTrue();
        result.FirstError.Should().Be(Errors.User.NotFound);
        _mockUserRepository.Verify(x => x.DeleteUser(It.IsAny<User>()), Times.Never);
    }

    [Theory]
    [MemberData(nameof(NotAdminUsers))]
    public async Task HandleDeleteCommand_WhenUserNotAdmin_ReturnAdminCannotBeDeletedError(User user)
    {
        //Arrange
        _mockUserRepository.Setup(x => x.GetUserByIdAsync(user.Id)).ReturnsAsync(user);

        //Act
        var result = await _handler.Handle(new DeleteUserCommand(user.Id), default);

        //Assert
        result.IsError.Should().BeTrue();
        result.FirstError.Should().Be(Errors.User.AdminCannotBeDeleted);
        _mockUserRepository.Verify(x => x.DeleteUser(user), Times.Never);
    }

    public static IEnumerable<object[]> NotAdminUsers()
    {
        yield return new object[] { CreateUserUtils.CreateTestUser(role: Role.Admin) };
    }
}