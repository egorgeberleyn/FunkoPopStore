using FluentValidation;
using FunkoPopStore.Domain.FigureAggregate.Enums;

namespace FunkoPopStore.Application.Figures.Commands.CreateFigure;

public class CreateFigureCommandValidator : AbstractValidator<CreateFigureCommand>
{
    public CreateFigureCommandValidator()
    {
        RuleFor(c => c.Name).NotEmpty();
        RuleFor(c => c.Price).NotEmpty().GreaterThan(0);
        RuleFor(c => c.ProductionYear).NotEmpty();
        RuleFor(c => c.Rarity).NotEmpty()
            .IsEnumName(typeof(Rarity), caseSensitive: false);
    }
}