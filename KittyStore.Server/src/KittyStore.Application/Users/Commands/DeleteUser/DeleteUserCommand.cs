using ErrorOr;
using KittyStore.Application.Common.SaveChangesPostProcessor;
using MediatR;

namespace KittyStore.Application.Users.Commands.DeleteUser
{
    public record DeleteUserCommand(Guid Id) : IRequest<ErrorOr<Unit>>, ICommand;
}