using FluentValidation;

namespace FunkoPopStore.Application.Authentication.Commands.RefreshToken;

public class RefreshTokenCommandValidator : AbstractValidator<RefreshTokenCommand>
{
    public RefreshTokenCommandValidator()
    {
        RuleFor(tokenCommand => tokenCommand.Token).NotEmpty();
        RuleFor(tokenCommand => tokenCommand.RefreshToken).NotEmpty();
    }
}