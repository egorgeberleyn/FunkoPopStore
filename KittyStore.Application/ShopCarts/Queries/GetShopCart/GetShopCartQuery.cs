using ErrorOr;
using KittyStore.Domain.ShopCartAggregate;
using KittyStore.Domain.UserAggregate.ValueObjects;
using MediatR;

namespace KittyStore.Application.ShopCarts.Queries.GetShopCart;

public record GetShopCartQuery(UserId UserId) : IRequest<ErrorOr<ShopCart>>;
