using FluentValidation;

namespace KittyStore.Application.Orders.Commands.CreateOrder;

public class CreateOrderValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderValidator()
    {
        RuleFor(ord => ord.AddressCommand.Country).MinimumLength(2)
            .Must(c => c.All(char.IsLetter));
        RuleFor(ord => ord.AddressCommand.City).MinimumLength(2)
            .Must(c => c.All(char.IsLetter));
        RuleFor(ord => ord.AddressCommand.Street).MinimumLength(2)
            .Must(c => c.All(char.IsLetter));
        RuleFor(ord => ord.AddressCommand.HouseNumber)
            .Must(c => c.All(char.IsLetterOrDigit));
    }
}