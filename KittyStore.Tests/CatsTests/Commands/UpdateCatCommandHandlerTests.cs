using KittyStore.Application.Cats.Commands.UpdateCat;
using KittyStore.Domain.CatAggregate.ValueObjects;
using KittyStore.Domain.Common.Errors;
using KittyStore.Tests.Common;
using Microsoft.EntityFrameworkCore;

namespace KittyStore.Tests.Cats.Commands;

public class UpdateCatCommandHandlerTests : TestCommandBase
{
    [Fact]
    public async Task DeleteCatCommandHandler_Success()
    {
        //Arrange
        var handler = new UpdateCatCommandHandler(CatRepository); //update gender, name and price
        const int age = 11;
        const string breed = "maine coon";
        const string color = "white";
        
        const string updateGender = "Female";
        const string updateName = "Gailee";
        const decimal updatePrice = 45;
        
        //Act
        var testCat = await handler.Handle(new UpdateCatCommand
            {
                Id = KittyContextFactory.CatIdForUpdate,
                Age = age,
                Breed = breed,
                Color = color,
                Gender = updateGender,
                Name = updateName,
                Price = updatePrice
            }, CancellationToken.None);
            
        //Assert
        Assert.NotNull(await Context.Cats.SingleOrDefaultAsync(cat => 
            cat.Id == KittyContextFactory.CatIdForUpdate &&
            cat.Price == updatePrice &&
            cat.Name == updateName &&
            cat.Gender.ToString() == updateGender));
    }

    [Fact]
    public async Task UpdateCatCommandHandler_FailOnWrongId()
    {
        //Arrange
        var handler = new UpdateCatCommandHandler(CatRepository);
        const int age = 11;
        const string breed = "maine coon";
        const string color = "white";
        
        const string updateGender = "Female";
        const string updateName = "Gailee";
        const decimal updatePrice = 45;
        
        //Act
        var testCat = await handler.Handle(new UpdateCatCommand
        {
            Id = CatId.CreateUnique(),
            Age = age,
            Breed = breed,
            Color = color,
            Gender = updateGender,
            Name = updateName,
            Price = updatePrice
        }, CancellationToken.None);
            
        //Assert
        Assert.Equal(Errors.Cat.NotFound, testCat.FirstError);
    }
    
}