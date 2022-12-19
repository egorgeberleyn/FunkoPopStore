using KittyStore.Application.Cats.Queries.GetAllCats;
using KittyStore.Domain.CatAggregate;
using KittyStore.Tests.Common;
using Shouldly;

namespace KittyStore.Tests.CatsTests.Queries;

public class GetCatsQueryHandlerTests : TestCommandBase
{
    [Fact]
    public async Task GetCatsQueryHandler_Success()
    {
        //Arrange
        var handler = new GetAllCatsQueryHandler(CatRepository);
        
        //Act
        var result = await handler.Handle(new GetAllCatsQuery(), 
            CancellationToken.None);
        
        //Assert
        result.Value.ShouldBeOfType<List<Cat>>();
        result.Value.Count.ShouldBe(7); //4 in entityConfig and 3 in testData
    }
}