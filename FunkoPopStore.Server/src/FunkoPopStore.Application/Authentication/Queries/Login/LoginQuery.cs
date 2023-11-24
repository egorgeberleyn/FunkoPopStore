using ErrorOr;
using FunkoPopStore.Application.Authentication.Common;
using MediatR;

namespace FunkoPopStore.Application.Authentication.Queries.Login;

public record LoginQuery(
    string Email,
    string Password) : IRequest<ErrorOr<AuthenticationResult>>;