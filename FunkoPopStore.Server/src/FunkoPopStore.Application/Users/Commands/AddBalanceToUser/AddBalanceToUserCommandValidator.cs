using FluentValidation;

namespace FunkoPopStore.Application.Users.Commands.AddBalanceToUser;

public class AddBalanceToUserCommandValidator : AbstractValidator<AddBalanceToUserCommand>
{
    public AddBalanceToUserCommandValidator()
    {
        RuleFor(b => b.Amount).GreaterThan(0);
    }
}