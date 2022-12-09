using FluentValidation;
using KittyStore.Domain.CatAggregate;

namespace KittyStore.Application.Cats.Commands.UpdateCat;

public class UpdateCatCommandValidator : AbstractValidator<UpdateCatCommand>
{
    public UpdateCatCommandValidator()
    {
        RuleFor(c => c.Age).NotEmpty().GreaterThan(0);
        RuleFor(c => c.Breed).NotEmpty();
        RuleFor(c => c.Color).NotEmpty();
        RuleFor(c => c.Name).NotEmpty();
        RuleFor(c => c.Price).NotEmpty().GreaterThan(0);
    }
}