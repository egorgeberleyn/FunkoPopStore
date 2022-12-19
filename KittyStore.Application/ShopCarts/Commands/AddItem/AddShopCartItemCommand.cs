using ErrorOr;
using KittyStore.Domain.CatAggregate.ValueObjects;
using KittyStore.Domain.ShopCartAggregate;
using KittyStore.Domain.UserAggregate.ValueObjects;
using MediatR;

namespace KittyStore.Application.ShopCarts.Commands.AddItem
{
    public record AddShopCartItemCommand(
        UserId UserId,
        decimal Price,
        CatId CatId) : IRequest<ErrorOr<ShopCart>>;
}
