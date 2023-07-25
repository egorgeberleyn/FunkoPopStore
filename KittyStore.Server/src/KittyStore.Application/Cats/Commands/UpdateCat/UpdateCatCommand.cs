using KittyStore.Domain.CatAggregate;
using MediatR;
using ErrorOr;
using KittyStore.Application.Common.SaveChangesPostProcessor;

namespace KittyStore.Application.Cats.Commands.UpdateCat
{
    public record UpdateCatCommand(
        Guid Id,
        string Name,
        int Age,
        string Color,
        string Breed,
        string Gender,
        decimal Price) : IRequest<ErrorOr<Cat>>, ICommand;
}