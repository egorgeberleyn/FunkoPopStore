using FunkoPopStore.Domain.OrderAggregate;

namespace FunkoPopStore.Application.Common.Interfaces.Persistence;

public interface IOrderRepository
{
    Task<List<Order>> GetUserOrdersAsync(Guid userId);

    Task<Order?> FindOrderByIdAsync(Guid orderId);

    Task CreateOrderAsync(Order newOrder);
}