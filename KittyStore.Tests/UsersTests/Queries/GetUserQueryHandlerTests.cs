using KittyStore.Application.Users.Queries.GetUserById;
using KittyStore.Domain.UserAggregate;
using KittyStore.Tests.Common;
using Shouldly;

namespace KittyStore.Tests.UsersTests.Queries;

public class GetUserQueryHandlerTests : TestCommandBase
{
    [Fact]
    public async Task GetCatsQueryHandler_Success()
    {
        //Arrange
        var handler = new GetUserByIdQueryHandler(UserRepository);
        
        //Act
        var result = await handler.Handle(new GetUserByIdQuery(AppDbContextFactory.UserAId), 
            CancellationToken.None);
        
        //Assert
        result.Value.ShouldBeOfType<User>();
        result.Value.LastName.ShouldBe("Pot");
        result.Value.Balance.Amount.ShouldBe(228);
    }
}