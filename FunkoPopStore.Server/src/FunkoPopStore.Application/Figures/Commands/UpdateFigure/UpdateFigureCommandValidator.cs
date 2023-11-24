using FluentValidation;
using FunkoPopStore.Domain.FigureAggregate.Enums;

namespace FunkoPopStore.Application.Figures.Commands.UpdateFigure;

public class UpdateFigureCommandValidator : AbstractValidator<UpdateFigureCommand>
{
    public UpdateFigureCommandValidator()
    {
        RuleFor(c => c.Name).NotEmpty();
        RuleFor(c => c.Price).NotEmpty().GreaterThan(0);
        RuleFor(c => c.Rarity).NotEmpty()
            .IsEnumName(typeof(Rarity), caseSensitive: false);
    }
}