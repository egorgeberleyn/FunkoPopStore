using FluentAssertions;
using KittyStore.Application.Cats.Commands.CreateCat;
using KittyStore.Domain.CatAggregate;

namespace KittyStore.Application.UnitTests.TestUtils.Cats.Extensions;

public static partial class CatExtensions
{
    public static void ValidateCreatedFrom(this Cat cat, CreateCatCommand command)
    {
        cat.Id.Should().NotBeEmpty();
        cat.Name.Should().Be(command.Name);
        cat.Age.Should().Be(command.Age);
        cat.Gender.ToString().Should().Be(command.Gender);
        cat.Breed.Should().Be(command.Breed);
        cat.Color.Should().Be(command.Color);
        cat.Price.Should().Be(command.Price);
    }
}