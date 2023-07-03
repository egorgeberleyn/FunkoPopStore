using ErrorOr;
using KittyStore.Domain.ShopCartAggregate;
using KittyStore.Domain.ShopCartAggregate.ValueObjects;
using KittyStore.Domain.UserAggregate.ValueObjects;
using MediatR;

namespace KittyStore.Application.ShopCarts.Commands.RemoveItem
{
    public record RemoveItemCommand(
        ShopCartItemId Id) : IRequest<ErrorOr<ShopCart>>;
}