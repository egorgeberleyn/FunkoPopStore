using KittyStore.Application.Authentication.Common;
using MediatR;
using ErrorOr;
using KittyStore.Application.Common.Interfaces.Authentication;
using KittyStore.Application.Common.Interfaces.Persistence;
using KittyStore.Application.Common.Interfaces.Utils;
using KittyStore.Domain.Common.Errors;

namespace KittyStore.Application.Authentication.Queries.Login
{
    public record LoginQuery(
        string Email,
        string Password) : IRequest<ErrorOr<AuthenticationResult>>;
}
