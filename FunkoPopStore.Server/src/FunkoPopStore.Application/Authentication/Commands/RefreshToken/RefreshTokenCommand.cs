using ErrorOr;
using FunkoPopStore.Application.Authentication.Common;
using FunkoPopStore.Application.Common.SaveChangesPostProcessor;
using MediatR;

namespace FunkoPopStore.Application.Authentication.Commands.RefreshToken;

public record RefreshTokenCommand(
    string Token,
    string RefreshToken) : IRequest<ErrorOr<AuthenticationResult>>, ICommand;