using KittyStore.Domain.OrderAggregate;
using KittyStore.Domain.UserAggregate.ValueObjects;

namespace KittyStore.Application.Common.Interfaces.Persistence;

public interface IOrderRepository
{
    Task<List<Order>> GetUserOrdersAsync(UserId id);

    Task CreateOrderAsync(Order newOrder);
}