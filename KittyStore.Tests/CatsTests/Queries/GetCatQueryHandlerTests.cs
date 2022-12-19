using KittyStore.Application.Cats.Queries.GetCat;
using KittyStore.Domain.CatAggregate;
using KittyStore.Tests.Common;
using Shouldly;

namespace KittyStore.Tests.Cats.Queries;

public class GetCatQueryHandlerTests : TestCommandBase
{
    [Fact]
    public async Task GetCatsQueryHandler_Success()
    {
        //Arrange
        var handler = new GetCatQueryHandler(CatRepository);
        
        //Act
        var result = await handler.Handle(new GetCatQuery(KittyContextFactory.CatIdForUpdate), 
            CancellationToken.None);
        
        //Assert
        result.Value.ShouldBeOfType<Cat>();
        result.Value.Age.ShouldBe(11);
    }
}