using KittyStore.Application.Cats.Commands.UpdateCat;
using KittyStore.Domain.CatAggregate.ValueObjects;
using KittyStore.Domain.Common.Errors;
using KittyStore.Tests.Common;
using Microsoft.EntityFrameworkCore;

namespace KittyStore.Tests.CatsTests.Commands;

public class UpdateCatCommandHandlerTests : TestCommandBase
{
    [Fact]
    public async Task DeleteCatCommandHandler_Success()
    {
        //Arrange
        var handler = new UpdateCatCommandHandler(CatRepository); //update gender, name and price
        var testCat = TestData.Cats[1];
        
        const string updateGender = "Female";
        const string updateName = "Gailee";
        const decimal updatePrice = 45;
        
        //Act
        var result = await handler.Handle(new UpdateCatCommand
            {
                Id = AppDbContextFactory.CatIdForUpdate,
                Age = testCat.Age,
                Breed = testCat.Breed,
                Color = testCat.Color,
                Gender = updateGender,
                Name = updateName,
                Price = updatePrice
            }, CancellationToken.None);
            
        //Assert
        Assert.NotNull(await Context.Cats.SingleOrDefaultAsync(cat => 
            cat.Id == AppDbContextFactory.CatIdForUpdate &&
            cat.Price == updatePrice &&
            cat.Name == updateName &&
            cat.Gender.ToString() == updateGender));
    }

    [Fact]
    public async Task UpdateCatCommandHandler_FailOnWrongId()
    {
        //Arrange
        var handler = new UpdateCatCommandHandler(CatRepository);
        var testCat = TestData.Cats[1];
        
        const string updateGender = "Female";
        const string updateName = "Gailee";
        const decimal updatePrice = 45;
        
        //Act
        var result = await handler.Handle(new UpdateCatCommand
        {
            Id = CatId.CreateUnique(),
            Age = testCat.Age,
            Breed = testCat.Breed,
            Color = testCat.Color,
            Gender = updateGender,
            Name = updateName,
            Price = updatePrice
        }, CancellationToken.None);
            
        //Assert
        Assert.Equal(Errors.Cat.NotFound,result.FirstError);
    }
    
}