using ErrorOr;
using KittyStore.Application.Authentication.Common;
using MediatR;

namespace KittyStore.Application.Authentication.Commands.Register;

public record RegisterCommand(
    string FirstName,
    string LastName,
    string Email,
    string Password) : IRequest<ErrorOr<AuthenticationResult>>;
