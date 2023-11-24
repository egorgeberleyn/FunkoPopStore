using FluentValidation;

namespace FunkoPopStore.Application.ShopCarts.Commands.AddItem;

public class AddShopCartItemCommandValidator : AbstractValidator<AddShopCartItemCommand>
{
    public AddShopCartItemCommandValidator()
    {
        RuleFor(c => c.CatId).NotEmpty();
    }
}