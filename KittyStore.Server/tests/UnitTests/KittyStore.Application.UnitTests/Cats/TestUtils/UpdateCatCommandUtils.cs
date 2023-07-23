using KittyStore.Application.Cats.Commands.UpdateCat;
using KittyStore.Application.UnitTests.TestUtils.Constants;

namespace KittyStore.Application.UnitTests.Cats.TestUtils;

public class UpdateCatCommandUtils
{
    public static UpdateCatCommand CreateCommand(Guid? id = null, string? name = null, int? age = null, 
        string? color = null, string? breed = null, string? gender = null, decimal? price = null) =>
        new()
        {
            Id = id ?? Constants.Cat.Id,
            Name = name ?? Constants.Cat.Name,
            Age = age ?? Constants.Cat.Age,
            Color = color ?? Constants.Cat.Color,
            Breed = breed ?? Constants.Cat.Breed,
            Gender = gender ?? Constants.Cat.Gender,
            Price = price ?? Constants.Cat.Price
        };
}