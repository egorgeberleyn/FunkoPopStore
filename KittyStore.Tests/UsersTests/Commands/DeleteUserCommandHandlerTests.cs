using KittyStore.Application.Users.Commands.DeleteUser;
using KittyStore.Domain.Common.Errors;
using KittyStore.Domain.UserAggregate.ValueObjects;
using KittyStore.Tests.Common;
using Microsoft.EntityFrameworkCore;

namespace KittyStore.Tests.UsersTests.Commands;

public class DeleteUserCommandHandlerTests : TestCommandBase
{
    [Fact]
    public async Task DeleteCatCommandHandler_Success()
    {
        //Arrange
        var handler = new DeleteUserCommandHandler(UserRepository);
        
        //Act
        await handler.Handle(new DeleteUserCommand(
                AppDbContextFactory.UserBId), 
            CancellationToken.None);
            
        //Assert
        Assert.Null(await Context.Users.SingleOrDefaultAsync(user => 
            user.Id == AppDbContextFactory.UserBId));
    }

    [Fact]
    public async Task DeleteCatCommandHandler_FailOnWrongId()
    {
        //Arrange
        var handler = new DeleteUserCommandHandler(UserRepository);
        
        //Act
        var result = await handler.Handle(new DeleteUserCommand(
                UserId.CreateUnique()), 
            CancellationToken.None);
            
        //Assert
        Assert.Equal(Errors.User.NotFound, result.FirstError);
    }
}