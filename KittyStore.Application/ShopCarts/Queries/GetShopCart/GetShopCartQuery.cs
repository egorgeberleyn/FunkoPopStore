using ErrorOr;
using KittyStore.Domain.ShopCartAggregate;
using MediatR;

namespace KittyStore.Application.ShopCarts.Queries.GetShopCart;

public record GetShopCartQuery() : IRequest<ErrorOr<ShopCart>>;