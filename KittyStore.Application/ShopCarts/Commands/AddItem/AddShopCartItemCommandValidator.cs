using FluentValidation;

namespace KittyStore.Application.ShopCarts.Commands.AddItem
{
    public class AddShopCartItemCommandValidator : AbstractValidator<AddShopCartItemCommand>
    {
        public AddShopCartItemCommandValidator()
        {
            RuleFor(c => c.Price).NotEmpty().GreaterThanOrEqualTo(0);
            RuleFor(c => c.CatId).NotEmpty();
        }
    }
}