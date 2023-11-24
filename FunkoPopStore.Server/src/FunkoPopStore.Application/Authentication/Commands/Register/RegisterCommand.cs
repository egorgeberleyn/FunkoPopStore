using ErrorOr;
using FunkoPopStore.Application.Authentication.Common;
using FunkoPopStore.Application.Common.SaveChangesPostProcessor;
using MediatR;

namespace FunkoPopStore.Application.Authentication.Commands.Register;

public record RegisterCommand(
    string FirstName,
    string LastName,
    string Email,
    string Password) : IRequest<ErrorOr<AuthenticationResult>>, ICommand;