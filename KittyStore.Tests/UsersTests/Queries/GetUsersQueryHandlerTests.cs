using KittyStore.Application.Users.Queries.GetAllUsers;
using KittyStore.Domain.UserAggregate;
using KittyStore.Tests.Common;
using Shouldly;

namespace KittyStore.Tests.UsersTests.Queries;

public class GetUsersQueryHandlerTests : TestCommandBase
{
    [Fact]
    public async Task GetCatsQueryHandler_Success()
    {
        //Arrange
        var handler = new GetAllUsersQueryHandler(UserRepository);
        
        //Act
        var result = await handler.Handle(new GetAllUsersQuery(), 
            CancellationToken.None);
        
        //Assert
        result.Value.ShouldBeOfType<List<User>>();
        result.Value.Count.ShouldBe(4);
    }
}