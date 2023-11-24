using ErrorOr;
using FunkoPopStore.Application.Common.SaveChangesPostProcessor;
using FunkoPopStore.Domain.ShopCartAggregate;
using MediatR;

namespace FunkoPopStore.Application.ShopCarts.Commands.AddItem;

public record AddShopCartItemCommand(
    Guid CatId) : IRequest<ErrorOr<ShopCart>>, ICommand;