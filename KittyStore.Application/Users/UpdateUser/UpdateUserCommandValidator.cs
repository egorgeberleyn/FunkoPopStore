using FluentValidation;

namespace KittyStore.Application.Users.UpdateUser;

public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {
        RuleFor(user => user.FirstName).NotEmpty();
        RuleFor(user => user.LastName).NotEmpty()
            .Must((user, lastName) => lastName != user.FirstName);
        RuleFor(user => user.Email).NotEmpty().EmailAddress();
        RuleFor(user => user.Balance).NotEmpty().GreaterThanOrEqualTo(0);
    }
}