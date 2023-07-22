using KittyStore.Application.Cats.Commands.CreateCat;
using KittyStore.Application.UnitTests.TestUtils.Constants;

namespace KittyStore.Application.UnitTests.Cats.TestUtils;

public class CreateCatCommandUtils
{
    public static CreateCatCommand CreateCommand(string? name = null, int? age = null, string? color = null, 
        string? breed = null, string? gender = null, decimal? price = null) =>
        new(
            name ?? Constants.Cat.Name,
            age ?? Constants.Cat.Age,
            color ?? Constants.Cat.Color,
            breed ?? Constants.Cat.Breed,
            gender ?? Constants.Cat.Gender,
            price ?? Constants.Cat.Price);
}