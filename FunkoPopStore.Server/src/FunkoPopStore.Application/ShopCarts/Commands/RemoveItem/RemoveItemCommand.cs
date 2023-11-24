using ErrorOr;
using FunkoPopStore.Application.Common.SaveChangesPostProcessor;
using FunkoPopStore.Domain.ShopCartAggregate;
using MediatR;

namespace FunkoPopStore.Application.ShopCarts.Commands.RemoveItem;

public record RemoveItemCommand(
    Guid Id) : IRequest<ErrorOr<ShopCart>>, ICommand;