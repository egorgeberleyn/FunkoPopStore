﻿using FluentAssertions;
using FunkoPopStore.Application.Common.Interfaces.Authentication;
using FunkoPopStore.Application.Common.Interfaces.Persistence;
using FunkoPopStore.Application.UnitTests.Users.UserUtils;
using FunkoPopStore.Application.Users.Commands.AddBalanceToUser;
using FunkoPopStore.Domain.Common.Errors;
using FunkoPopStore.Domain.UserAggregate;
using FunkoPopStore.Domain.UserAggregate.Enums;
using FunkoPopStore.Domain.UserAggregate.ValueObjects;
using Moq;

namespace FunkoPopStore.Application.UnitTests.Users.Commands.AddBalanceToUser;

public class AddBalanceToUserHandlerTests
{
    private readonly Mock<IUserRepository> _mockUserRepository;
    private readonly Mock<ICurrentUserService> _mockCurrentUserService;
    private readonly AddBalanceToUserCommandHandler _handler;

    public AddBalanceToUserHandlerTests()
    {
        _mockCurrentUserService = new Mock<ICurrentUserService>();
        _mockUserRepository = new Mock<IUserRepository>();
        _handler = new AddBalanceToUserCommandHandler(_mockUserRepository.Object, _mockCurrentUserService.Object);
    }

    [Fact]
    public async Task HandleAddBalanceToUser_WhenUserNotFound_ReturnNotFoundError()
    {
        //Arrange
        _mockCurrentUserService.Setup(x => x.GetUserAsync()).ReturnsAsync((User?)null);

        //Act
        var result = await _handler.Handle(new AddBalanceToUserCommand(100), default);

        //Accept
        result.IsError.Should().BeTrue();
        result.FirstError.Should().Be(Errors.User.NotFound);
        _mockUserRepository.Verify(x => x.UpdateUser(It.IsAny<User>()), Times.Never);
        _mockUserRepository.VerifyNoOtherCalls();
    }

    [Theory]
    [MemberData(nameof(ValidUsers))]
    public async Task HandleAddBalanceToUser_WhenUserIsValid_ReturnUserWithAddedBalance(User user)
    {
        //Arrange
        _mockCurrentUserService.Setup(x => x.GetUserAsync()).ReturnsAsync(user);

        //Act
        var result = await _handler.Handle(new AddBalanceToUserCommand(100), default);

        //Accept
        result.IsError.Should().BeFalse();
        result.Value.Balance?.Amount.Should().BeOneOf(1500, 200);
        _mockUserRepository.Verify(x => x.UpdateUser(user), Times.Once);
        _mockUserRepository.VerifyNoOtherCalls();
    }

    public static IEnumerable<object[]> ValidUsers()
    {
        yield return new object[] { CreateUserUtils.CreateTestUser(balance: Balance.Create(Currency.Dollar, 1400)) };
        yield return new object[] { CreateUserUtils.CreateTestUser(balance: Balance.Create(Currency.Euro, 100)) };
    }
}