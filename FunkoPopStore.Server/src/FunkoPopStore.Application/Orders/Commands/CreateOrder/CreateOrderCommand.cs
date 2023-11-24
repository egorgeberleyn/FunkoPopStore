using ErrorOr;
using FunkoPopStore.Application.Common.SaveChangesPostProcessor;
using FunkoPopStore.Domain.OrderAggregate;
using MediatR;

namespace FunkoPopStore.Application.Orders.Commands.CreateOrder;

public record CreateOrderCommand(
    AddressCommand AddressCommand) : IRequest<ErrorOr<Order>>, ICommand;

public record AddressCommand(
    string Country,
    string City,
    string Street,
    string HouseNumber);