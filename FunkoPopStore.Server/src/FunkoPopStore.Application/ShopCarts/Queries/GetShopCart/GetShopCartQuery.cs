using ErrorOr;
using FunkoPopStore.Domain.ShopCartAggregate;
using MediatR;

namespace FunkoPopStore.Application.ShopCarts.Queries.GetShopCart;

public record GetShopCartQuery() : IRequest<ErrorOr<ShopCart>>;