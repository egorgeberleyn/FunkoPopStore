using KittyStore.Application.Users.Commands.UpdateUser;
using KittyStore.Domain.Common.Errors;
using KittyStore.Domain.UserAggregate.ValueObjects;
using KittyStore.Tests.Common;
using Microsoft.EntityFrameworkCore;

namespace KittyStore.Tests.UsersTests.Commands;

public class UpdateUserCommandHandlerTests : TestCommandBase
{
    [Fact]
    public async Task UpdateUserCommandHandler_Success()
    {
        //Arrange
        var handler = new UpdateUserCommandHandler(UserRepository);
        var testUser = TestData.Users[0];
        const string updateFirstName = "Bob";

        //Act
        var result = await handler.Handle(
            new UpdateUserCommand
            {
                Id = testUser.Id,
                Balance = new BalanceCommand(testUser.Balance.Currency.ToString(), testUser.Balance.Amount),
                Email = testUser.Email,
                FirstName = updateFirstName,
                LastName = testUser.LastName
            }, CancellationToken.None);
            
        //Assert
        Assert.NotNull(await Context.Users.SingleOrDefaultAsync(user => 
            user.Id == testUser.Id &&
            user.FirstName == updateFirstName));
    }
    
    [Fact]
    public async Task UpdateUserCommandHandler_FailOnWrongId()
    {
        //Arrange
        var handler = new UpdateUserCommandHandler(UserRepository);
        var testUser = TestData.Users[0];
        const string updateFirstName = "Bob";

        //Act
        var result = await handler.Handle(
            new UpdateUserCommand
            {
                Id = UserId.CreateUnique(),
                Balance = new BalanceCommand(testUser.Balance.Currency.ToString(), testUser.Balance.Amount),
                Email = testUser.Email,
                FirstName = updateFirstName,
                LastName = testUser.LastName
            }, CancellationToken.None);
            
        //Assert
        Assert.Equal(Errors.User.NotFound, result.FirstError);
    }
}