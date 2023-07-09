using ErrorOr;
using KittyStore.Domain.OrderAggregate;
using MediatR;

namespace KittyStore.Application.Orders.Commands.CreateOrder
{
    public record CreateOrderCommand (
        AddressCommand AddressCommand) : IRequest<ErrorOr<Order>>;

    public record AddressCommand( 
        string Country, 
        string City,
        string Street,
        string HouseNumber);
}