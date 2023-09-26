using ErrorOr;
using KittyStore.Application.Authentication.Common;
using KittyStore.Application.Common.SaveChangesPostProcessor;
using MediatR;

namespace KittyStore.Application.Authentication.Commands.Register
{
    public record RegisterCommand(
        string FirstName,
        string LastName,
        string Email,
        string Password) : IRequest<ErrorOr<AuthenticationResult>>, ICommand;
}