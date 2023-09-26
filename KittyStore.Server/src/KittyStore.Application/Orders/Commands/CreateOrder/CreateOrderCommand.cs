using ErrorOr;
using KittyStore.Application.Common.SaveChangesPostProcessor;
using KittyStore.Domain.OrderAggregate;
using MediatR;

namespace KittyStore.Application.Orders.Commands.CreateOrder
{
    public record CreateOrderCommand(
        AddressCommand AddressCommand) : IRequest<ErrorOr<Order>>, ICommand;

    public record AddressCommand(
        string Country,
        string City,
        string Street,
        string HouseNumber);
}