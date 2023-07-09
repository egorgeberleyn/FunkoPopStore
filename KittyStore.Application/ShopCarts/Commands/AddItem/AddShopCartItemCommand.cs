using ErrorOr;
using KittyStore.Domain.ShopCartAggregate;
using MediatR;

namespace KittyStore.Application.ShopCarts.Commands.AddItem
{
    public record AddShopCartItemCommand(
        Guid CatId) : IRequest<ErrorOr<ShopCart>>;
}
