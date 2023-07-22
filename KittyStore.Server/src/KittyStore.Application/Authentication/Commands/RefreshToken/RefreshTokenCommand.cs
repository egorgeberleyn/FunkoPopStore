using KittyStore.Application.Common.SaveChangesPostProcessor;
using MediatR;
using ErrorOr;
using KittyStore.Application.Authentication.Common;

namespace KittyStore.Application.Authentication.Commands.RefreshToken;

public record RefreshTokenCommand(
    string Token, 
    string RefreshToken) : IRequest<ErrorOr<AuthenticationResult>>, ICommand;
