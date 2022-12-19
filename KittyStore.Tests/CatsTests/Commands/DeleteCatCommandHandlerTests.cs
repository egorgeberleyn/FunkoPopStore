using KittyStore.Application.Cats.Commands.DeleteCat;
using KittyStore.Domain.CatAggregate.ValueObjects;
using KittyStore.Domain.Common.Errors;
using KittyStore.Tests.Common;
using Microsoft.EntityFrameworkCore;

namespace KittyStore.Tests.CatsTests.Commands;

public class DeleteCatCommandHandlerTests : TestCommandBase
{
    [Fact]
    public async Task DeleteCatCommandHandler_Success()
    {
        //Arrange
        var handler = new DeleteCatCommandHandler(CatRepository);
        
        //Act
         await handler.Handle(new DeleteCatCommand(
                KittyContextFactory.CatIdForDelete), 
                CancellationToken.None);
            
        //Assert
        Assert.Null(await Context.Cats.SingleOrDefaultAsync(cat => 
            cat.Id == KittyContextFactory.CatIdForDelete));
    }

    [Fact]
    public async Task DeleteCatCommandHandler_FailOnWrongId()
    {
        //Arrange
        var handler = new DeleteCatCommandHandler(CatRepository);
        
        //Act
        var testCat = await handler.Handle(new DeleteCatCommand(
                CatId.CreateUnique()), 
            CancellationToken.None);
            
        //Assert
        Assert.Equal(Errors.Cat.NotFound, testCat.FirstError);
    }
}