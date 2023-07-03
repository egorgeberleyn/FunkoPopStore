using ErrorOr;
using KittyStore.Domain.OrderAggregate;
using KittyStore.Domain.UserAggregate.ValueObjects;
using MediatR;

namespace KittyStore.Application.Orders.Queries.GetAllUserOrders
{
    public record GetAllUserOrdersQuery() : IRequest<ErrorOr<List<Order>>>;
}
