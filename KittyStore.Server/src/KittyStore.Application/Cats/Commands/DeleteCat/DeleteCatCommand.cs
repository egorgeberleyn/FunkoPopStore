using ErrorOr;
using KittyStore.Application.Common.SaveChangesPostProcessor;
using MediatR;

namespace KittyStore.Application.Cats.Commands.DeleteCat
{
    public record DeleteCatCommand(Guid Id) : IRequest<ErrorOr<Unit>>, ICommand;
}