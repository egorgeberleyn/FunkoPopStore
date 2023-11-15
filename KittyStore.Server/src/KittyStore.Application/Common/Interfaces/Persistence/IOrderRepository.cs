using KittyStore.Domain.OrderAggregate;

namespace KittyStore.Application.Common.Interfaces.Persistence
{
    public interface IOrderRepository
    {
        Task<List<Order>> GetUserOrdersAsync(Guid userId);

        Task<Order?> FindOrderByIdAsync(Guid orderId);

        Task CreateOrderAsync(Order newOrder);
    }
}