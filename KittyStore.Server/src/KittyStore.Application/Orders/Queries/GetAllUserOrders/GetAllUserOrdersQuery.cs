using ErrorOr;
using KittyStore.Domain.OrderAggregate;
using MediatR;

namespace KittyStore.Application.Orders.Queries.GetAllUserOrders
{
    public record GetAllUserOrdersQuery() : IRequest<ErrorOr<List<Order>>>;
}