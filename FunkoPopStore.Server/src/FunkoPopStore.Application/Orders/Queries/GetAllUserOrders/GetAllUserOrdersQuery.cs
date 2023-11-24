using ErrorOr;
using FunkoPopStore.Domain.OrderAggregate;
using MediatR;

namespace FunkoPopStore.Application.Orders.Queries.GetAllUserOrders;

public record GetAllUserOrdersQuery() : IRequest<ErrorOr<List<Order>>>;