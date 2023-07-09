using ErrorOr;
using KittyStore.Domain.ShopCartAggregate;
using MediatR;

namespace KittyStore.Application.ShopCarts.Commands.RemoveItem
{
    public record RemoveItemCommand(
        Guid Id) : IRequest<ErrorOr<ShopCart>>;
}