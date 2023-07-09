using ErrorOr;
using MediatR;

namespace KittyStore.Application.Users.Commands.DeleteUser
{
    public record DeleteUserCommand(Guid Id) : IRequest<ErrorOr<Unit>>;
}