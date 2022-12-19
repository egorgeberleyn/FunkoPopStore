using MediatR;
using ErrorOr;
using KittyStore.Domain.CatAggregate;

namespace KittyStore.Application.Cats.Commands.CreateCat
{
    public record CreateCatCommand(
        string Name,
        int Age,
        string Color,
        string Breed,
        string Gender,
        decimal Price) : IRequest<ErrorOr<Cat>>;
}
