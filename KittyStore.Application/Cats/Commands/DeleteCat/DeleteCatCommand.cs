using ErrorOr;
using MediatR;

namespace KittyStore.Application.Cats.Commands.DeleteCat
{
    public record DeleteCatCommand(Guid Id) : IRequest<ErrorOr<Unit>>;
}
