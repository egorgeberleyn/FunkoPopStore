using ErrorOr;
using KittyStore.Domain.CatAggregate.ValueObjects;
using KittyStore.Domain.ShopCartAggregate;
using MediatR;

namespace KittyStore.Application.ShopCarts.Commands.AddItem
{
    public record AddShopCartItemCommand(
        decimal Price,
        CatId CatId) : IRequest<ErrorOr<ShopCart>>;
}
