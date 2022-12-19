using KittyStore.Application.Cats.Commands.CreateCat;
using KittyStore.Tests.Common;
using Microsoft.EntityFrameworkCore;

namespace KittyStore.Tests.CatsTests.Commands;

public class CreateCatCommandHandlerTests : TestCommandBase
{
    [Fact]
    public async Task CreateCatCommandHandler_Success()
    {
        //Arrange
        var handler = new CreateCatCommandHandler(CatRepository);
        const int age = 10;
        const string breed = "breed";
        const string color = "color";
        const string gender = "Male";
        const string name = "name";
        const decimal price = 100;

        //Act
        var testCat = await handler.Handle(
            new CreateCatCommand(name, age, color, breed, gender, price), CancellationToken.None);
            
        //Assert
        Assert.NotNull(await Context.Cats.SingleOrDefaultAsync(cat => 
            cat.Id == testCat.Value.Id &&
            cat.Name == name &&
            cat.Age == testCat.Value.Age &&
            cat.Price == price &&
            cat.Breed == breed &&
            cat.Gender.ToString() == gender &&
            cat.Color == color));
    }
}