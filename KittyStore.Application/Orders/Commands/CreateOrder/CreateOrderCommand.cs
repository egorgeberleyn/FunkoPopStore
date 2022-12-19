using ErrorOr;
using KittyStore.Domain.OrderAggregate;
using KittyStore.Domain.UserAggregate.ValueObjects;
using MediatR;

namespace KittyStore.Application.Orders.Commands.CreateOrder
{
    public record CreateOrderCommand (
        AddressCommand AddressCommand,
        UserId UserId) : IRequest<ErrorOr<Order>>;

    public record AddressCommand( 
        string Country, 
        string City,
        string Street,
        string HouseNumber);
}